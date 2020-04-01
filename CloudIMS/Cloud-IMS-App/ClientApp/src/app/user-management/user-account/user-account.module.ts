import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { UserAccountComponent } from './user-account.component';
import { AddEditUserAccountComponent } from './addEdit-user-account.component';
import { UserAuthorizationService } from '../../services/UserAuthorization.service';
import { UserAccountService } from '../../services/UserAccount.service';
import { UserGroupService } from '../../services/UserGroup.service';



@NgModule({
  declarations: [
    UserAccountComponent,
    AddEditUserAccountComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: 'user-management/user-account', component: UserAccountComponent },
      { path: 'user-management/user-account/add', component: AddEditUserAccountComponent },
      { path: 'user-management/user-account/edit/:id', component: AddEditUserAccountComponent }
    ])
  ],
  providers: [UserAuthorizationService, UserAccountService, UserGroupService]
})
export class UserAccountModule { }
