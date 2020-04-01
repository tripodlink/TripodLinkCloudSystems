import { Component, OnInit } from '@angular/core';
import { AppComponent } from '../app.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {

  constructor(public app: AppComponent) {
   
  }

  ngOnInit() {
    this.app.isLogin = true;
  }
  public btn_login_click() {
    this.app.notLogin();
  }
}
