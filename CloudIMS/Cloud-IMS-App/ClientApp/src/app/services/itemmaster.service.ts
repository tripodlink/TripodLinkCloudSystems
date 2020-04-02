import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IiTemMaster } from '../classes/data-dictionary/ItemMaster/IitemMaster.interface';
import { IiTemGroup } from '../classes/data-dictionary/ItemGroup/IitemGroup.interface';
import { IUnitCode } from '../classes/data-dictionary/UnitCode/IUnitCode.interface';
import { IiTemMasterUnit } from '../classes/data-dictionary/ItemMasterUnit/IitemMasterUnit.interface';
import { IiTemMasterUnitJoinUnit } from '../classes/data-dictionary/ItemMasterUnit/IitemMasterUnitJoinUnit.interface';

import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

@Injectable()
export class ItemMasterServices {

  url: string = 'api/itemmaster';

  urlig: string = 'api/itemgroup';

  urluc: string = 'api/unitcode';

  urlsup: string = 'api/supplier';

  urlitmu: string = 'api/itemmasterunit';

  constructor(private _http: HttpClient) {
  }

  getItemMasterData(): Observable<IiTemMaster[]> {
    return this._http.get<IiTemMaster[]>(this.url);
  }

  getItemGroupData(): Observable<IiTemGroup[]> {
    return this._http.get<IiTemGroup[]>(this.urlig);
  }

  getUnitCodeData(): Observable<IUnitCode[]> {
    return this._http.get<IUnitCode[]>(this.urluc);
  }

  getItemMasterUnitByID(id: string): Observable<IiTemMasterUnitJoinUnit[]> {
    let params = new HttpParams().set('id', id);
    return this._http.get<IiTemMasterUnitJoinUnit[]>(this.urlitmu + "/FindID", { params: params });

  }

  insertItemMaster(itemMaster: IiTemMaster) {
    const headers = new HttpHeaders({
      "Content-Type": "application/json",
      "Accept": "application/json"
    });
    return this._http.post<IiTemMaster>(this.url + "/Add", JSON.stringify(itemMaster), { headers: headers });

  }

  insertItemMasterUnit(itemMasterUnit: IiTemMasterUnit) {
    const headers = new HttpHeaders({
      "Content-Type": "application/json",
      "Accept": "application/json"
    });
    return this._http.post<IiTemMasterUnit>(this.urlitmu + "/Add", JSON.stringify(itemMasterUnit), { headers: headers });

  }


  updateItemMaster(itemMaster: IiTemMaster) {
    const headers = new HttpHeaders({
      "Content-Type": "application/json",
      "Accept": "application/json"
    });
    return this._http.post<IiTemMaster>(this.url + "/Update", JSON.stringify(itemMaster), { headers: headers });

  }

  updateItemMasterUnit(itemMasterUnit: IiTemMasterUnit) {
    const headers = new HttpHeaders({
      "Content-Type": "application/json",
      "Accept": "application/json"
    });
    return this._http.post<IiTemMaster>(this.urlitmu + "/Update", JSON.stringify(itemMasterUnit), { headers: headers });

  }

  deleteItemMaster(id: string): Observable<string> {
    let params = new HttpParams().set('id', id);
    return this._http.delete<string>(this.url + "/Delete" , { params: params });

  }

  deleteItemMasterUnit(id: string,unit: string): Observable<string> {
    let params = new HttpParams().set('id', id).set('unit', unit);
    return this._http.delete<string>(this.urlitmu + "/Delete", { params: params });
  }


  deleteAllItemMasterUnit(id: string): Observable<string> {
    let params = new HttpParams().set('id', id);
    return this._http.delete<string>(this.urlitmu + "/DeleteAll", { params: params });
  }

}
