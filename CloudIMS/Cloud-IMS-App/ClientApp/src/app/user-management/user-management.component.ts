import { Component, OnInit } from '@angular/core';
import { ProgramMenu } from '../classes/ProgramMenu';
import { UserAuthorizationService } from '../services/UserAuthorization.service';
import { UserAccount } from '../classes/UserAccount';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html'
})
export class UserManagementComponent implements OnInit {
  um_pg_menus: ProgramMenu[] = new Array();
  message: string;

  constructor(private _userAuthorizationService: UserAuthorizationService) {
  }

  ngOnInit(): void {
    let user: UserAccount = this._userAuthorizationService.getCurrentUser("User Management");

    if (user != null) {
      this.um_pg_menus = user.programMenus.filter(pm => pm.programFolderID == "UM");
    }
  }
}
