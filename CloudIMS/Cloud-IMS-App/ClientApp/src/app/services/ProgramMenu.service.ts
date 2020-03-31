import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserGroup } from './../classes/UserGroup';

@Injectable()
export class ProgramMenuService {
  url: string = 'api/programmenu';


  constructor(private _http: HttpClient) { }

  getAllUserGroups(): Observable<UserGroup[]> {
    return this._http.get<UserGroup[]>(this.url);
  }
}
