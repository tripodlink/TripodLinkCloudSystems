import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { UserAuthorizationService } from '../../services/UserAuthorization.service';
import { UserGroupComponent } from './user-group.component';
import { UserGroupService } from '../../services/UserGroup.service';



@NgModule({
  declarations: [
    UserGroupComponent,    
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: 'user-management/user-group', component: UserGroupComponent }
    ])
  ],
  providers: [UserAuthorizationService, UserGroupService]
})
export class UserGroupModule { }
