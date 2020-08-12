import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserAccount } from './../../classes/UserAccount';
import { UserGroup } from './../../classes/UserGroup';
import { UserAccountService } from './../../services/UserAccount.service';
import { UserGroupService } from './../../services/UserGroup.service';
import { ToastrService } from 'ngx-toastr';
import { FormGroup, FormControl, FormBuilder, FormArray } from '@angular/forms';
import { UserAuthorizationService } from './../../services/UserAuthorization.service';

@Component({
  selector: "add-edit-user",
  templateUrl: "./addEdit-user-account.component.html"
})
export class AddEditUserAccountComponent {
  private _currentUser: UserAccount;
  isNewUser: boolean = true;
  user: UserAccount = new UserAccount();
  addEditForm: FormGroup;

  userGroups: UserGroup[] = new Array();

  constructor(private _activatedRoute: ActivatedRoute,
    private _userAccountService: UserAccountService,
    private _toastr: ToastrService,
    private _userGroupService: UserGroupService,
    private _userAuthorizationService: UserAuthorizationService,
    private formBuilder: FormBuilder) {

    this.isNewUser = true;
    this.createForm();

  }

  private createForm() {
    this.addEditForm = this.formBuilder.group({
      userId: [''],
      userName: [''],
      password: [''],
      isActive: [true],
      userGroups: new FormArray([])
    });
  }

  private createAndAddUserGroupsForm(ugList: UserGroup[]): void {
    ugList.forEach(ug =>
      this.addUserGroupForm(ug)
    );
  }

  private addUserGroupForm(ug: UserGroup): void {
    let ugForm = this.formBuilder.group({
      id: [ug.id],
      name: [ug.name],
      isSelected: [false]
    });

    (this.addEditForm.controls.userGroups as FormArray).push(ugForm);
  }

  checkUserGroupsFormList(ugList: UserGroup[]): void {
    ugList.forEach(ug => {
      this.checkUserGroupForm(ug.id);
    });
  }

  checkUserGroupForm(ugId: string): void {
    (this.addEditForm.controls.userGroups as FormArray).controls.forEach((ugForm: FormGroup) => {
      if (ugForm.controls.id.value == ugId) {
        ugForm.controls.isSelected.setValue(true);
      }
    });
  }

  ngOnInit() {
    this._userAuthorizationService.getCurrentUser()
      .then(user => {
        this._currentUser = user;
      })
      .catch((error) => {
        // error while retrieving user data

      })

    this.getAllUserGroups();

    let id: string = this._activatedRoute.snapshot.params["id"];

    if (id != null) {
      this._userAccountService.findUserById(id.toUpperCase())
        .subscribe(userData => {
          this.user = userData;
          this.isNewUser = false;

          this.addEditForm.get('userId').setValue(this.user.userID);
          this.addEditForm.get('userName').setValue(this.user.userName);
          this.addEditForm.get('password').setValue(this.user.password);
          this.addEditForm.get('isActive').setValue(this.user.isActive);

          this.checkUserGroupsFormList(this.user.userGroups);
        }
          , error => {
            this._toastr.error(error.error, "Error");
          }
        );
    }
  }

  getAllUserGroups(): void {
    this._userGroupService.getAllUserGroups().subscribe(userGroupData => {
      this.userGroups = userGroupData;

      this.createAndAddUserGroupsForm(this.userGroups);
    }, error => {
      this._toastr.error(error.error, "Error");
      this._toastr.clear();
    });

  }
  
  
  saveUserDetails(): void {
    let userid: string = this.addEditForm.controls["userId"].value;
    userid = userid.toUpperCase();

    this.addEditForm.controls["userId"].valid
    this.user.userID = userid;
    this.user.userName = this.addEditForm.get("userName").value;
    this.user.password = this.addEditForm.controls.password.value;
    this.user.isActive = this.addEditForm.controls["isActive"].value;
    this.user.createdBy = this._currentUser.userID;
    this.user.updatedBy = this._currentUser.userID;
    this.user.userGroups = this.getSelectedUserGroups();

    if (this.isNewUser) {
      this.addUser(this.user);
    } else {
      this.updateUser(this.user);
    }

  }

  getSelectedUserGroups(): UserGroup[] {
    let ugList: UserGroup[] = new Array();

    (this.addEditForm.controls.userGroups as FormArray).controls.forEach((ugForm: FormGroup) => {

      if ((ugForm.controls.isSelected.value as boolean) == true) {
        let ug = new UserGroup();
        ug.id = ugForm.controls.id.value;
        ug.name = ugForm.controls.name.value

        ugList.push(ug);
      }

    });

    return ugList;
  }

  addUser(user: UserAccount): void {
    this._userAccountService.addUser(user).subscribe(sucess => {
      this.isNewUser = false;
      this._toastr.info("User saved successfully", "Save");
    }, error => {
      this._toastr.error(error.error, "Save");
    });
  }

  updateUser(user: UserAccount): void {
    this._userAccountService.updateUser(user).subscribe(sucess => {
      this.isNewUser = false;
      this._toastr.info("User updated successfully", "Save");
    }, error => {
      this._toastr.error(error.error, "Save");
    });
  }


  isGrantedUserGroup(groupId: string): boolean {
    let index: number = this.user.userGroups.findIndex(ug => ug.id == groupId);

    if (index > -1) {
      return true;
    } else {
      return false;
    }
  }

  //deleteUser(): boolean {
  //  this._userAccountService.deleteUser(this.user.userID).subscribe(data =>
  //  {
  //    this.clearEntry();
  //    this._toastr.info("User deleted successfully.", "Delete")
  //  }, error =>
  //    {
  //      this._toastr.error(error.error);
  //  });

  //  return false;
  //}

  clearEntry(): boolean {

      this.user = new UserAccount();

      this.addEditForm.controls.userId.setValue("");
      this.addEditForm.controls.userName.setValue("");
      this.addEditForm.controls.password.setValue("");
      this.addEditForm.controls.isActive.setValue(true);

      (this.addEditForm.controls.userGroups as FormArray).controls.forEach((ugForm: FormGroup) => {
        ugForm.controls.isSelected.setValue(false);
      });

      return false;

  }
}

