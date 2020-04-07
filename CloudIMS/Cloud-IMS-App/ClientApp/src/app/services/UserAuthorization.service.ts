import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserAccount } from './../classes/UserAccount'
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { promise } from 'protractor';


@Injectable()
export class UserAuthorizationService {
  private currentUser: UserAccount;
  private loginErrorMessage: string = "";
  private userAccountUrl: string = 'api/useraccount';

  constructor(private toastr: ToastrService,
    private router: Router,
    private _http: HttpClient) {
  }


  setCurrentUser(user: UserAccount): void {
    this.currentUser = user;
  }


  getCurrentUser(): Promise<UserAccount> {
    let _user = this.currentUser;
    let _http = this._http;
    let _url = this.userAccountUrl;

    return new Promise(function (resolve, reject) {
      if (_user) {
        resolve(_user);
      } else {
        let userId: string = localStorage.getItem('userId');
        let params = new HttpParams().set('id', userId);

        return _http.get<UserAccount>(_url + "/Find", { params: params })
          .subscribe(userdata => {
            resolve(userdata);
          }, error => {
              reject(error);
          });
      }
    });
  }


  private async isValidUserToken(userId: string, token: string): Promise<boolean> {

    let user = { userID: userId, token: token }

    await this._http.post<UserAccount>(this.userAccountUrl + "/validate-user-token", user)
      .subscribe(userData => {
        this.currentUser = userData;
        this.loginErrorMessage = "";

        localStorage.setItem('userId', this.currentUser.userID)
        localStorage.setItem('token', this.currentUser.token)

        sessionStorage.setItem('userId', this.currentUser.userID)
        sessionStorage.setItem('token', this.currentUser.token)
        
        console.log({ module: "Validate Token", user: this.currentUser });

      }, error => {
        this.currentUser = null;
        this.loginErrorMessage = error.error;

          console.log({ module: "Validate Token", error: error })
      })

    return false;
  }

  setLoginErrorMessage(message: string): void {
    this.loginErrorMessage = message;
  }

  getLoginErrorMessage(): string {
    return this.loginErrorMessage;
  }

  getUserToken() {
    return null;
  }

  doLogin(userId: string, password: string): Observable<UserAccount> {
    let user = { userID: userId, password: password }
    return this._http.post<UserAccount>(this.userAccountUrl + "/authenticate", user);
  }

  doLogout(): void {
    this.currentUser = null
    this.loginErrorMessage = ''

    localStorage.removeItem('userId')
    localStorage.removeItem('token')
  }

  isLoggedIn(): boolean {
    let userId: string = localStorage.getItem('userId');
    let token: string = localStorage.getItem('token');

    if (userId == null || token == null) {
      return false
    }

    return true;
  }
}
