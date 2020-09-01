import { Component, OnInit } from "@angular/core";
import { UserGroup } from './../../classes/UserGroup';
import { UserGroupService } from './../../services/UserGroup.service';
import { ToastrService } from 'ngx-toastr';
import { ProgramFolder } from "../../classes/ProgramFolder";
import { ProgramMenu } from "../../classes/ProgramMenu";
import { FormGroup, FormBuilder, FormControl, FormArray } from "@angular/forms";


@Component({
  selector: "user-group",
  templateUrl: "./user-group.component.html"
})
export class UserGroupComponent {
  userGroups: UserGroup[]=new Array();
  programFoldersWithMenus: ProgramFolder[] = new Array();
  selectedUserGroup: UserGroup = new UserGroup();
  isNewUserGroup: boolean = true;
  addEditUserGroupForm: FormGroup;

  constructor(public _userGroupService: UserGroupService,
    public _toastr: ToastrService,
    public _formBuilder: FormBuilder) {

    this.createForm();
  }

  createForm(): void {
    this.addEditUserGroupForm = this._formBuilder.group({
      groupId: [''],
      groupName: [''],
      groupIsApprover: [''],
      programFolders: new FormArray([])
    });
  }

  createFoldersForm(pf: ProgramFolder): FormGroup {
    let pfForm: FormGroup = this._formBuilder.group({
      id: [pf.id],
      name: [pf.name],
      checked: false,
      programMenus: new FormArray([]),
    });
    ///////////////////////////////////
    //(pfForm.controls.id as FormControl).setValue(pf.id);
    //(pfForm.controls.name as FormControl).setValue(pf.name);
    //(pfForm.controls.checked as FormControl).setValue(false);
    //////////////////////////////////////
    pf.programMenus.forEach((pm) => {
      let pmArray = pfForm.controls.programMenus as FormArray;
      let pmForm: FormGroup = this.createProgramForm(pm, pfForm);

      pmArray.push(pmForm);
    });

    return pfForm;
  }

  createProgramForm(pm: ProgramMenu, pfForm: FormGroup): FormGroup {
    let pmForm: FormGroup = this._formBuilder.group({
      id: [pm.id],
      name: [pm.name],
      checked: [false]

    });

    //(pmForm.controls.id as FormControl).setValue(pm.id);
    //(pmForm.controls.name as FormControl).setValue(pm.name);
    //(pmForm.controls.checked as FormControl).setValue(false);

    return pmForm;
  }

  onProgramFolderCheckChanged(pfForm: FormGroup): void {        
    let isChecked = !pfForm.controls.checked.value as boolean;

    let pmForms = (pfForm.controls.programMenus as FormArray).controls;

    pmForms.forEach((p) => {
      let pmF = p as FormGroup;

      (pmF.controls.checked as FormControl).setValue(isChecked);
    });
  }


  onProgramMenuCheckChanged(pmForm: FormGroup, pfForm: FormGroup): void {
    let isChecked = !pmForm.controls.checked.value as boolean;
    
    if (isChecked) {
      pfForm.controls.checked.setValue(isChecked);
    } else {
      let hasCheckedItem: boolean = false;

      let pmForms = (pfForm.controls.programMenus as FormArray).controls;

      Array.from(pmForms).forEach(pm => {
        let pmF = pm as FormGroup;

        if (pmF.controls.id.value != pmForm.controls.id.value) {
          if ((pmF.controls.checked.value as boolean) == true) {
            hasCheckedItem = true;
            return;
          }
        }

      });

      pfForm.controls.checked.setValue(hasCheckedItem);
    }
  }

  ngOnInit(): void {
    this.getAllUserGroups();
    this.getAllProgramFoldersWithMenus();
  }


  getAllUserGroups() : void {
    this._userGroupService.getAllUserGroups().subscribe(userGroupData => {
      this.userGroups = userGroupData;
    }, error => {
      this._toastr.error(error.error);
    });

  }


  getAllProgramFoldersWithMenus(): void {
    this._userGroupService.getProgramFoldersWithMenus().subscribe(pfData => {
      this.programFoldersWithMenus = pfData;
      this.createProgramMenuFormArray();
    }, error => {
      this._toastr.error(error.error);
    });
  }

  createProgramMenuFormArray(): void {
    this.programFoldersWithMenus.sort((a, b) => a.sequenceNo < b.sequenceNo ? -1 : a.sequenceNo > b.sequenceNo ? 1 : 0);

    let pfArray = this.addEditUserGroupForm.controls.programFolders as FormArray;

    this.programFoldersWithMenus.forEach((pf) => {
      let pfForm: FormGroup = this.createFoldersForm(pf);
      pfArray.push(pfForm);
    });
  }

  getFolderIcon(pf: ProgramFolder): string {
    if (pf.icon != null && pf.icon != '') {
      return pf.icon;
    }

       return "fa fa-folder-open";
  }

  getProgramIcon(pm: ProgramMenu): string {
    if (pm.iconName != null && pm.iconName != '') {
      return pm.iconName;
    }

    return "fa fa-columns";
  }

  saveUserGroupDetails(): void {
    let groupId = this.addEditUserGroupForm.controls.groupId.value;
    groupId = groupId.toUpperCase();

    this.selectedUserGroup.id = groupId;
    this.selectedUserGroup.name = this.addEditUserGroupForm.controls.groupName.value;
    this.selectedUserGroup.isApprover = this.addEditUserGroupForm.controls.groupIsApprover.value;
    this.selectedUserGroup.programMenus = this.getProgramMenusForSaving();


    if (this.isNewUserGroup) {
      this.addNewUserGroup();
    } else {
      this.updateUserGroup();
    }

  }

  getProgramMenusForSaving(): ProgramMenu[] {
    let programMenus: ProgramMenu[] = new Array();

    let pfForms = (this.addEditUserGroupForm.controls.programFolders as FormArray).controls;

    //loop to all folders
    pfForms.forEach((pfForm: FormGroup) => {

      let pmForms = (pfForm.controls.programMenus as FormArray).controls;

      //loop to all program menus
      pmForms.forEach((pmForm: FormGroup) => {
        if ((pmForm.controls.checked.value as boolean) == true) {
          let pm = new ProgramMenu();
          pm.programFolderID = pfForm.controls.id.value;
          pm.id = pmForm.controls.id.value;
          pm.name = pmForm.controls.name.value;

          programMenus.push(pm);
        }
      });

    });

    return programMenus;
  }

  addNewUserGroup() {
    this._userGroupService.addUserGroup(this.selectedUserGroup).subscribe(
      userGroupData => {
        let ug = new UserGroup();
        ug.id = this.selectedUserGroup.id;
        ug.name = this.selectedUserGroup.name;

        this.userGroups.push(ug);

        this._toastr.info("User group saved successfully.", "Save");
        this.isNewUserGroup = false;
      },
      error => {
        this._toastr.error(error.error, "Save");
      }
    );
  }

  updateUserGroup() {
    this._userGroupService.updateUserGroup(this.selectedUserGroup).subscribe(
      userGroupData => {
        this.addEditUserGroupForm.controls.groupName.setValue(this.selectedUserGroup.name);

        let td = document.getElementsByClassName("user-group-name-td-" + this.selectedUserGroup.id)[0] as HTMLElement;
        td.innerHTML = this.selectedUserGroup.name;

        this._toastr.info("User group uUpdated successfully.", "Save");
        this.isNewUserGroup = false;
      },
      error => {
        this._toastr.error(error.error, "Save");
      }
    );
  }

  deleteUserGroup(): boolean {
    if (confirm("Do you want to delete user group '" + this.selectedUserGroup.name + "'. Are you sure?")) {
      this._userGroupService.deleteUserGroup(this.selectedUserGroup.id).subscribe(data => {

        let index: number = this.userGroups.findIndex(ug => ug.id == this.selectedUserGroup.id);

        if (index > -1) {
          this.userGroups.splice(index, 1);
        }

        this.clearEntry();

        this._toastr.info("User group delete successfully.", "Delete");
      },
        error => {
          this._toastr.error(error.error, "Delete");
        }
      );
    }
       
    return false;
  }

  displayUserGroupDetails(ug_id: string): void {
    if (ug_id != this.selectedUserGroup.id) {
      this.clearEntry();

      this._userGroupService.getUserGroupDetails(ug_id).subscribe(ugData => {
        this.selectedUserGroup = ugData;
        this.isNewUserGroup = false;
        
        this.addEditUserGroupForm.controls.groupId.setValue(this.selectedUserGroup.id);
        this.addEditUserGroupForm.controls.groupName.setValue(this.selectedUserGroup.name);
        this.addEditUserGroupForm.controls.groupIsApprover.setValue(this.selectedUserGroup.isApprover);
        this.displayUserGroupGrantedPrograms();
      },
        error => {
          this._toastr.error(error.error);
        });

    }
  }

  displayUserGroupGrantedPrograms(): void {
    //checked all granted program folders
    if (this.selectedUserGroup.programFolders != null) {
      this.selectedUserGroup.programFolders.forEach(pf => {
        let pfForm: FormGroup = this.getProgramFolderForm(pf.id);

        if (pfForm != null) {
          pfForm.controls.checked.setValue(true);
        }
      });
    }


    //checked all granted program menus
    if (this.selectedUserGroup.programMenus != null) {
      this.selectedUserGroup.programMenus.forEach(pm => {
        let pfForm: FormGroup = this.getProgramFolderForm(pm.programFolderID);

        if (pfForm != null) {
          let pmForm: FormGroup = this.getProgramMenuForm(pm.id, pfForm);

          if (pmForm != null) {
            pmForm.controls.checked.setValue(true);
          }
        }
      });
    }
  }

  getProgramFolderForm(pfId: string): FormGroup {
    let returnFrm: FormGroup;

    let pfForms = (this.addEditUserGroupForm.controls.programFolders as FormArray).controls

    pfForms.forEach((pfForm: FormGroup) => {
      if (pfForm.controls.id.value == pfId) {
        returnFrm = pfForm;        
      }

      if (returnFrm != null) {
        return;
      }
    });

    return returnFrm;
  }

  getProgramMenuForm(pmId: string, pfForm: FormGroup): FormGroup {
    let returnFrm: FormGroup;

    let pmForms = (pfForm.controls.programMenus as FormArray).controls;

    pmForms.forEach((pmForm: FormGroup) => {
      if (pmForm.controls.id.value == pmId) {
        returnFrm = pmForm;
      }

      if (returnFrm != null) {
        return;
      }
    });

    return returnFrm;
  }

  clearEntry(): boolean {
    this.selectedUserGroup = new UserGroup();
    this.isNewUserGroup = true;

    this.addEditUserGroupForm.controls.groupId.setValue("");
    this.addEditUserGroupForm.controls.groupName.setValue("");
    this.addEditUserGroupForm.controls.groupIsApprover.setValue(false);

    let pfForms = (this.addEditUserGroupForm.controls.programFolders as FormArray).controls;

    //loop to all folders
    pfForms.forEach((pfForm: FormGroup) => {
      pfForm.controls.checked.setValue(false);

      let pmForms = (pfForm.controls.programMenus as FormArray).controls;

      //loop to all program menus
      pmForms.forEach((pmForm: FormGroup) => {
        pmForm.controls.checked.setValue(false);
      });

    });

    return false;
  }
}
