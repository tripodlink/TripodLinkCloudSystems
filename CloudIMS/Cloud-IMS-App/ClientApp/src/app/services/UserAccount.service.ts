import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserAccount } from '../classes/UserAccount'
import { RequestOptions, URLSearchParams, ResponseContentType } from '@angular/http';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class UserAccountService {
  url: string = 'api/useraccount';

  constructor(private _http: HttpClient, private _toastr: ToastrService) {

  }

  getAllUsers(): Observable<UserAccount[]> {
    return this._http.get<UserAccount[]>(this.url);
  }

  findUserById(id: string): Observable<UserAccount> {
    let params = new HttpParams().set('id', id);

    return this._http.get<UserAccount>(this.url + "/Find", { params: params });
  }

  findUserByIdOrName(search_key: string): Observable<UserAccount[]> {
    let params = new HttpParams().set('search_key', search_key);

    return this._http.get<UserAccount[]>(this.url + "/findUserIdOrName", { params: params });
  }

  addUser(user: UserAccount) {
    const headers = new HttpHeaders({
      "Content-Type": "application/json",
      "Accept": "application/json"
    });

    return this._http.post<UserAccount[]>(this.url + "/Add", JSON.stringify(user), { headers: headers });
  }

  updateUser(user: UserAccount) {
    const headers = new HttpHeaders({
      "Content-Type": "application/json",
      "Accept": "application/json"
    });

    return this._http.post<UserAccount[]>(this.url + "/update", JSON.stringify(user), { headers: headers });
  }

  deleteUser(id: string): Observable<string> {
    const headers = new HttpHeaders({
      "Content-Type": "application/json",
      "Accept": "application/text"
    });

    let params = new HttpParams().set('id', id);

    return this._http.delete<string>(this.url + "/delete", { params: params, headers: headers });
  }

}
