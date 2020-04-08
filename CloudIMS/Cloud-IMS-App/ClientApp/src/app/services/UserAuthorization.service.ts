import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserAccount } from './../classes/UserAccount'
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { promise } from 'protractor';
import { CookieService } from 'ngx-cookie-service';


@Injectable()
export class UserAuthorizationService {
  private currentUser: UserAccount;
  private loginErrorMessage: string = "";
  private userAccountUrl: string = 'api/useraccount';

  constructor(private toastr: ToastrService,
    private router: Router,
    private _http: HttpClient,
    private cookieService: CookieService) {
  }


  setCurrentUser(user: UserAccount): void {
    this.currentUser = user;
  }


  getCurrentUser(): Promise<UserAccount> {
    let _user = this.currentUser;
    let _http = this._http;
    let _url = this.userAccountUrl;
    let _cookieService = this.cookieService;

    return new Promise(function (resolve, reject) {
      if (_user) {
        resolve(_user);
      } else {
        let userId: string = _cookieService.get('userId');
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

  setLoginCookieValue(userId: string, token: string) {
    this.cookieService.set('userId', userId);
    this.cookieService.set('token', token);
  }

  doLogout(): void {
    this.currentUser = null
    this.loginErrorMessage = ''

    this.cookieService.delete('userId');
    this.cookieService.delete('token');
  }

  isLoggedIn(): boolean {
    let cookieUserId: string = this.cookieService.get('userId');
    let cookieToken: string = this.cookieService.get('token');

    if (cookieUserId && cookieToken) {
      return true;
    } else {
      return false;
    }
  }

  isAuthenticated(): Promise<boolean> {
    let cookieUserId: string = this.cookieService.get('userId');
    let cookieToken: string = this.cookieService.get('token');

    let _auth = this;

    return new Promise(function (resolve, reject) {
      if (cookieUserId == null || cookieToken == null) {
        // there are no user or token in Cookie
        resolve(false);
      }

      if (_auth.currentUser && _auth.currentUser.userID === cookieUserId && _auth.currentUser.token === cookieToken) {
        // the user ID and token in cookie matches the current user -> valid login
        resolve(true)
      }

      if (cookieUserId && cookieToken) {
        // re-authenticate user
        let userToAuthenticate = { userId: cookieUserId, token: cookieToken };

        _auth._http.post<UserAccount>(_auth.userAccountUrl + "/reauthenticate-user", userToAuthenticate)
          .subscribe(userData => {
            _auth.currentUser = userData;
            _auth.loginErrorMessage = "";

            _auth.setLoginCookieValue(userData.userID, userData.token);
          }, error => {
              _auth.loginErrorMessage = error.message;
              resolve(false);
          });
      }
    });
  }
}
