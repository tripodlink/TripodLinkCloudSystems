import { Component, OnInit} from '@angular/core';
import { UserAuthorizationService } from '../services/UserAuthorization.service';
import { UserAccount } from '../classes/UserAccount';
import { Router } from '@angular/router';

@Component({
  selector: 'app-app-sidebar-menu',
  templateUrl: './app-sidebar-menu.component.html'
})
export class AppSidebarMenuComponent implements OnInit {
  user: UserAccount;
  message: string;
  button_status: string;
  button_isActive: boolean;

  constructor(private auth: UserAuthorizationService, private router: Router) {  }

  ngOnInit(): void {
    this.user = this.auth.getCurrentUser("SideBar");
  }

  navigate(url: string): void {
    this.router.navigateByUrl(url)
  }

  private IsButtonActive() {
    this.button_status = "active";
    this.button_isActive = true;
  }

}
