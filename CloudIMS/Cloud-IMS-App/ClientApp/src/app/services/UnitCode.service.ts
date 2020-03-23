import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IUnitCode } from '../classes/IUnitCode.interface'

import { map, filter, switchMap } from 'rxjs/operators';

@Injectable()
export class  UnitCodeService{

  constructor(private _http: HttpClient) {
  }

  getUnitCodes(): Observable<IUnitCode[]> {
    return this._http.get<IUnitCode[]>('http://localhost:59303/unitcode');
  }
}
