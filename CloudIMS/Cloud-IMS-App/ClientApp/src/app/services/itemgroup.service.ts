import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IiTemGroup } from '../classes/IitemGroup.interface';

@Injectable()
export class ItemGroupServices {

  constructor(private _http: HttpClient) {
  }
  GetItemGroupData(): Observable<IiTemGroup[]> {
    return this._http.get<IiTemGroup[]>('api/itemgroup');
  }
}
