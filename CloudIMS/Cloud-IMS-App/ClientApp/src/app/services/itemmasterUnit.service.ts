import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IiTemMasterUnit } from '../classes/data-dictionary/ItemMasterUnit/IitemMaster.interface';


import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

@Injectable()
export class ItemMasterUnitServices {

  url: string = 'api/itemmasterunit';

  constructor(private _http: HttpClient) {
  }

  getItemMasterData(): Observable<IiTemMasterUnit[]> {
    return this._http.get<IiTemMasterUnit[]>(this.url);
  }

  getDistinctIndex() {
    return this._http.get<string[]>(this.url + "/DistinctIndex");

  }

  getItemMasterUnitByID(id: string): Observable<IiTemMasterUnit[]> {
    let params = new HttpParams().set('id', id);
    return this._http.get<IiTemMasterUnit[]>(this.url + "/FindID", { params: params });

  }

  insertItemMaster(itemMaster: IiTemMasterUnit) {
    const headers = new HttpHeaders({
      "Content-Type": "application/json",
      "Accept": "application/json"
    });
    return this._http.post<IiTemMasterUnit>(this.url + "/Add", JSON.stringify(itemMaster), { headers: headers });

  }

  updateItemMaster(itemMaster: IiTemMasterUnit) {
    const headers = new HttpHeaders({
      "Content-Type": "application/json",
      "Accept": "application/json"
    });
    return this._http.post<IiTemMasterUnit>(this.url + "/Update", JSON.stringify(itemMaster), { headers: headers });

  }


  deleteItemMaster(id: string): Observable<string> {
    let params = new HttpParams().set('id', id);
    return this._http.delete<string>(this.url + "/Delete" , { params: params });

  }
}
