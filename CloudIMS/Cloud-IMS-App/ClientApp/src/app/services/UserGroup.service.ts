import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserGroup } from './../classes/UserGroup';
import { ProgramFolder } from '../classes/ProgramFolder';

@Injectable()
export class UserGroupService {
  userGroupUrl: string = 'api/usergroup';
  programMenuUrl: string = 'api/programmenu';


  constructor(private _http: HttpClient) { }

  getAllUserGroups(): Observable<UserGroup[]> {
    return this._http.get<UserGroup[]>(this.userGroupUrl);
  }

  getProgramFolders(): Observable<ProgramFolder[]> {
    return this._http.get<ProgramFolder[]>(this.programMenuUrl + "/get-all-program-folders");
  }

  getProgramFoldersWithMenus(): Observable<ProgramFolder[]> {
    return this._http.get<ProgramFolder[]>(this.programMenuUrl + "/get-all-program-folders-with-menus");
  }

  getUserGroupDetails(ug_id: string): Observable<UserGroup> {
    const headers = new HttpHeaders({
      "Content-Type": "application/json",
      "Accept": "application/text"
    });

    let params = new HttpParams().set('id', ug_id);

    return this._http.get<UserGroup>(this.userGroupUrl + "/get-user-group-with-folders-and-menus", { params: params, headers: headers });
  }

  addUserGroup(ug: UserGroup): Observable<UserGroup> {
    const headers = new HttpHeaders({
      "Content-Type": "application/json",
      "Accept": "application/json"
    });

    return this._http.post<UserGroup>(this.userGroupUrl + '/add', JSON.stringify(ug), { headers: headers });
  }

  updateUserGroup(ug: UserGroup): Observable<UserGroup> {
    const headers = new HttpHeaders({
      "Content-Type": "application/json",
      "Accept": "application/json"
    });


    return this._http.post<UserGroup>(this.userGroupUrl + '/update', JSON.stringify(ug), { headers: headers });
  }

  deleteUserGroup(id: string): Observable<string> {
    const headers = new HttpHeaders({
      "Content-Type": "application/json",
      "Accept": "application/text"
    });

    let params = new HttpParams().set('id', id);

    return this._http.delete<string>(this.userGroupUrl + "/delete", { params: params, headers: headers });
  }
}
