import { Component, OnInit } from '@angular/core';
import { AppComponent } from '../app.component';
import { FormGroup, FormBuilder } from '@angular/forms';
import { UserAuthorizationService } from '../services/UserAuthorization.service';
import { Router } from '@angular/router';
import { UserAccount } from '../classes/UserAccount';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;

  constructor(public app: AppComponent,
    private fb: FormBuilder,
    private auth: UserAuthorizationService,
    private router: Router,
    private toastr: ToastrService) {

    this.loginForm = this.fb.group({
      userId: [''],
      password: [''],
      message: [''],
      isStaySignedIn: [false]
    })

    this.loginForm.controls.userId.valueChanges.subscribe(value => this.onUsernameAndPasswordValueChanged())
    this.loginForm.controls.password.valueChanges.subscribe(value => this.onUsernameAndPasswordValueChanged())
  }

  onUsernameAndPasswordValueChanged() {
    this.loginForm.controls.message.setValue("")
  }

  ngOnInit() {
  }

  onBtnLoginClick2() {
    let userId = (this.loginForm.controls.userId.value as string).trim().toUpperCase()
    let password = (this.loginForm.controls.password.value as string)

    this.auth.doLogin(userId, password).subscribe(userData => {
      let user: UserAccount = userData;

      this.auth.setCurrentUser(user);
      this.auth.setLoginErrorMessage("")

      localStorage.setItem('userId', user.userID)
      localStorage.setItem('token', user.token)

      console.log({ module: "Authenticate", user: user });

      this.router.navigateByUrl("/dashboard")
    }, error => {
        this.auth.setCurrentUser(null);
        this.auth.setLoginErrorMessage(error.error);

        console.log({ module: "Authenticate", error: error })
    })

    return false
  }
}
