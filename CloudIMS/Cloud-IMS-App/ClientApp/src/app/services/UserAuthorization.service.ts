import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserAccount } from '../classes/UserAccount';
import { UserAccountService } from './UserAccount.service';

@Injectable()
export class UserAuthorizationService {
  currentUser: UserAccount;

  constructor(private _userAccountService: UserAccountService) {
    this._userAccountService.findUserById("SYSAD").subscribe(userdata => { this.currentUser = userdata });

    if (this.currentUser == null) {
      this.currentUser = new UserAccount();
      this.currentUser.userID = "SYSAD";
      this.currentUser.userName = "SYSTEM ADMINISTRATOR";
    }
  }
}
