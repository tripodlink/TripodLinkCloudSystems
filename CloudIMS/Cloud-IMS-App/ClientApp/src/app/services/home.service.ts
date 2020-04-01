import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IHome } from '../classes/home/IHome.interface';

@Injectable()
export class HomeService {

  constructor(private _http: HttpClient) {
  }
  getProgramMenu(): Observable<IHome[]> {
    return this._http.get<IHome[]>('api/home');
  }
}
