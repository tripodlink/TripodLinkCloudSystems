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
    this._userAuthorizationService.getCurrentUser()
      .then(user => {
        this.um_pg_menus = user.programMenus.filter(pm => pm.programFolderID == "UM")
          .sort((pm, pm2) => pm.sequenceNo - pm2.sequenceNo);
      })
      .catch((error) => {
        // error while retrieving user data
      })
  }
}
