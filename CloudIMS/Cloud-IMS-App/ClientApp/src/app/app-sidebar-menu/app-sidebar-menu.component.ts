import { Component} from '@angular/core';
import { SideBarService } from '../services/SideBar.service';
import { UserAuthorizationService } from '../services/UserAuthorization.service';
import { ProgramFolder } from '../classes/ProgramFolder';
@Component({
  selector: 'app-app-sidebar-menu',
  templateUrl: './app-sidebar-menu.component.html'
})
export class AppSidebarMenuComponent{

  pf_folders: ProgramFolder[];
  message: string;
  button_status: string;
  button_isActive: boolean;
  
  constructor(private sidebarS: SideBarService, private _userAuthorizationService: UserAuthorizationService) {
  }

  ngOnInit(): void {
    this.pf_folders = this._userAuthorizationService.currentUser.programFolders;

  }

  private IsButtonActive() {
    this.button_status = "active";
    this.button_isActive = true;
  }

}
