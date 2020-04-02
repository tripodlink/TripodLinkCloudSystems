import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';


// user management folder and services
import { UserManagementService } from '../services/UserManagement.service';
import { UserManagementComponent } from './user-management.component';

// user account components and services
import { UserAccountService } from '../services/UserAccount.service';
import { UserAccountComponent } from './user-account/user-account.component';
import { AddEditUserAccountComponent } from './user-account/addEdit-user-account.component';

// user group components and services
import { UserGroupService } from '../services/UserGroup.service';
import { UserGroupComponent } from './user-group/user-group.component';




@NgModule({
  declarations: [
    UserManagementComponent,
    UserAccountComponent,
    AddEditUserAccountComponent,
    UserGroupComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,

    RouterModule.forRoot([])
  ],
  providers: [UserManagementService, UserAccountService, UserGroupService]
})
export class UserManagementModule { }
