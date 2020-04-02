import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UserManagementComponent } from './user-management.component';
import { RouterModule } from '@angular/router';
import { UserAccountModule } from './user-account/user-account.module';
import { UserManagementService } from '../services/UserManagement.service';
import { UserGroupModule } from './user-group/user-group.module'


@NgModule({
  declarations: [UserManagementComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,

    UserAccountModule,
    UserGroupModule,

    RouterModule.forRoot([
      {path: 'user-management',component: UserManagementComponent}
    ])
  ],
  providers:[UserManagementService]
})
export class UserManagementModule { }
