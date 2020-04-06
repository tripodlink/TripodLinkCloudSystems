import { Component, OnInit } from '@angular/core';
import { UserAuthorizationService } from '../services/UserAuthorization.service';
import { UserAccount } from '../classes/UserAccount';

@Component({
  selector: 'app-app-navbar-menu',
  templateUrl: './app-navbar-menu.component.html'
})
export class AppNavbarMenuComponent implements OnInit {
  user: UserAccount;

  constructor(private auth: UserAuthorizationService) { }

  ngOnInit(): void {
    this.user = this.auth.getCurrentUser("Nav Bar");;
    }

}
