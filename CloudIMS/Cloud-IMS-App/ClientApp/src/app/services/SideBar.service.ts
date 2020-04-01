import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ISideBar } from '../classes/app-sidebar-menu/ISideBar.interface';

@Injectable()
export class SideBarService {

  constructor(private _http: HttpClient) {
  }
  getSideBar(): Observable<ISideBar[]> {
    return this._http.get<ISideBar[]>('api/sidebar');
  }
}
