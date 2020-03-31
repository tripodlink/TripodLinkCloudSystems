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
      userId: new FormControl(),
      userName: new FormControl(),
      password: new FormControl()
    });
  }

  ngOnInit() {
    this.getAllUserGroups();

    let id: string = this._activatedRoute.snapshot.params["id"];

    if (id != null) {
      this._userAccountService.findUserById(id.toUpperCase())
        .subscribe(userData => {
          this.user = userData;
          this.isNewUser = false;

          this.addEditForm.get('userID').setValue(this.user.userID);
          this.addEditForm.get('userName').setValue(this.user.userName);
          this.addEditForm.get('password').setValue(this.user.password);

        }
          , error => {
            this._toastr.error(error.error, "Error");
            this._toastr.clear();
          }
        );
    }
  }

  getAllUserGroups(): void {
    this._userGroupService.getAllUserGroups().subscribe(userGroupData => {
      this.userGroups = userGroupData;

      //this.addUserGroupCheckboxes();
    }, error => {
      this._toastr.error(error.error, "Error");
      this._toastr.clear();
    });

  }
  

  private addUserGroupCheckboxes() {
    if (this.userGroups != null) {
      this.userGroups.forEach((o, i) => {
        const control = new FormControl(false); // if first item set to true, else false
        (this.addEditForm.controls.userGroupArray as FormArray).push(control);
      });
    }
  }
  
  saveUserDetails(): void {
    let userid: string = this.addEditForm.controls["userID"].value;
    userid = userid.toUpperCase();

    this.user.userID = userid;
    this.user.userName = this.addEditForm.controls["userName"].value;
    this.user.password = this.addEditForm.controls["password"].value;

    let isActiveEl = document.getElementsByClassName("is-active-user-checkbox").item(0) as HTMLInputElement;
    this.user.isActive = isActiveEl.checked;

    this.user.createdBy = this._userAuthorizationService.currentUser.userID;
    this.user.updatedBy = this._userAuthorizationService.currentUser.userID;
    this.user.userGroups = [];

    let ugElements = document.getElementsByClassName("user-group-input-checkbox");

    Array.from(ugElements).forEach((element: HTMLInputElement) => {
      let groupId: string = element.value;
      let isChecked: boolean = element.checked;

      if (isChecked) {
        if (isChecked) {
          this.addUserGroup(groupId);
        }
      }
    });

    if (this.isNewUser) {
      this.addUser(this.user);
    } else {
      this.updateUser(this.user);
    }
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

  private addUserGroup(groupId: string): void  {
    let ug = this.userGroups.find(ug => ug.id == groupId);

    this.user.userGroups.push(ug);
  }

  


  isGrantedUserGroup(groupId: string): boolean {
    let index: number = this.user.userGroups.findIndex(ug => ug.id == groupId);

    if (index > -1) {
      return true;
    } else {
      return false;
    }
  }

}

class MyUserFormGroup extends FormGroup {
  userGroupCheckBoxes: FormControl[];
}
