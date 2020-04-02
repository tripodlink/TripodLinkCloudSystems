import { Component } from '@angular/core';
import { AppComponent } from '../app.component';

@Component({
  selector: 'app-app-navbar-menu',
  templateUrl: './app-navbar-menu.component.html'
})
export class AppNavbarMenuComponent {
  constructor(public login: AppComponent) {

  }
  public btn_logout_click() {
    this.login.isLogin = true;
  }
}
