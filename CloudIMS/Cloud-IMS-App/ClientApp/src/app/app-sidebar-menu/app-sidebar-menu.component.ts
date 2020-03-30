import { Component} from '@angular/core';
import { SideBarService } from '../services/SideBar.service';
import { ISideBar } from '../classes/ISideBar.interface';
@Component({
  selector: 'app-app-sidebar-menu',
  templateUrl: './app-sidebar-menu.component.html'
})
export class AppSidebarMenuComponent{

  pf_folders: ISideBar[];
  message: string;

  constructor(private sidebarS: SideBarService) {
  }

  ngOnInit(): void {
    this.sidebarS.getSideBar().subscribe((pf_folders) => this.pf_folders = pf_folders); 
  }

}
