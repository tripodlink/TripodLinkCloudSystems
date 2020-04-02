import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserAccount } from './../classes/UserAccount'
import { UserAccountService } from './../services/UserAccount.service'
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Injectable()
export class UserAuthorizationService {
  private currentUser: UserAccount;
  private loginErrorMessage: string = "";

  constructor(private _userAccountService: UserAccountService,
    private toastr: ToastrService,
    private router: Router
    private jwt: j) {
  }

  getCurrentUser() {
    if (this.currentUser == null) {
      this._getCurrentUser();
    }

    return this.currentUser;
  }

  private async _getCurrentUser(): Promise<UserAccount> {
    let userId: string = 'SYSAD'


    return await this._userAccountService.findUserById(userId)
      .toPromise()
      .then(userData => this.currentUser = userData)
  }

  getLoginErrorMessage(): string {
    return this.loginErrorMessage;
  }

  doLogin(userId: string, password: string): void {
    this._userAccountService.findUserById(userId).subscribe(user => {
      if (user == null) {
        this.loginErrorMessage = "Invalid username or password"
      } else if (user.password !== password) {
        this.loginErrorMessage = "Invalid username or password"
      } else {
        console.log({ module: "Auth", user : user });

        this.currentUser = user;
        this.loginErrorMessage = ""

        this.router.navigateByUrl("/dashboard")
      }
    }, error => {
      this.toastr.error(error.erro)
        this.loginErrorMessage = "Server error."
    })
  }

  doLogout(): void {
    this.currentUser = null
    this.loginErrorMessage = ''
  }

  isLoggedIn(): boolean {
    if (this.getCurrentUser() != null) {
      return true
    } else {
      return false
    }
  }
}
