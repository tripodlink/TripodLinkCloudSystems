import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IiTemGroup } from '../classes/data-dictionary/ItemGroup/IitemGroup.interface';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
@Injectable()
export class ItemGroupServices {

  url: string = 'api/itemgroup';

  constructor(private _http: HttpClient) {
  }

  getItemGroupData(): Observable<IiTemGroup[]> {
    return this._http.get<IiTemGroup[]>(this.url);
  }

  insertItemGroup(itemgroup: IiTemGroup) {
    const headers = new HttpHeaders({
      "Content-Type": "application/json",
      "Accept": "application/json"
    });
    return this._http.post<IiTemGroup>(this.url + "/Add", JSON.stringify(itemgroup), { headers: headers });

  }

  updateItemGroup(itemgroup: IiTemGroup) {
    const headers = new HttpHeaders({
      "Content-Type": "application/json",
      "Accept": "application/json"
    });
    return this._http.post<IiTemGroup>(this.url + "/Update", JSON.stringify(itemgroup), { headers: headers });

  }


  deleteItemGroup(id: string): Observable<string> {
    let params = new HttpParams().set('id', id);
    return this._http.delete<string>(this.url + "/Delete" , { params: params });

  }
}
