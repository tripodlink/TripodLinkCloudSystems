import { Component } from '@angular/core';
import { UserAuthorizationService } from '../services/UserAuthorization.service';

@Component({
  selector: 'app-app-navbar-menu',
  templateUrl: './app-navbar-menu.component.html'
})
export class AppNavbarMenuComponent {
  constructor(private auth: UserAuthorizationService) {
    console.log({ module: "Nav Bar", user: auth.getCurrentUser() })
  }
}
