import { Component, OnInit } from '@angular/core';
import { ProgramMenu } from '../classes/ProgramMenu';
import { UserAuthorizationService } from '../services/UserAuthorization.service';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html'
})
export class UserManagementComponent implements OnInit {
  um_pg_menus: ProgramMenu[];
  message: string;

  constructor(private _userAuthorizationService: UserAuthorizationService) {
  }

  ngOnInit(): void {
    this.um_pg_menus = this._userAuthorizationService.getCurrentUser().programMenus.filter(pm => pm.programFolderID == "UM");
  }
}
