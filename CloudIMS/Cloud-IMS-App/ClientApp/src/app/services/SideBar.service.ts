import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ISideBar } from '../classes/app-sidebar-menu/ISideBar.interface';

@Injectable()
export class SideBarService {
  url: string = 'api/sidebar';
  constructor(private _http: HttpClient) {
  }
  getSideBar(id: string): Observable<ISideBar[]> {
    let params = new HttpParams().set('id', id);

    return this._http.get<ISideBar[]>(this.url + "/Index", {params: params });
  }
}
