import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IUserManagement } from '../classes/user-management/IUserManagement.interface';

@Injectable()
export class UserManagementService {
  constructor(private _http: HttpClient) {
  }
  getProgramMenu(): Observable<IUserManagement[]> {
    return this._http.get<IUserManagement[]>('api/usermanagement');
  }
}
