import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { itemLotNo, ItemTracking } from '../classes/inventory-management/itemTracking/itemTracking.interface';
import { IDepartment } from '../classes/data-dictionary/Department/IDepartment.interface';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
@Injectable()
export class ItemTrackingServices {

  urlDep: string = 'api/department';

  url: string = 'api/itemtracking';

  constructor(private _http: HttpClient) {
  }

  getItemTrackingData(ItemID: string, lotNum: string): Observable<ItemTracking[]> {
    let params = new HttpParams().set("ItemID", ItemID).set('lotNum', lotNum);
    return this._http.get<ItemTracking[]>(this.url + "/GetItemData", { params: params });
  }

  getLotNum(itemID: string ): Observable<itemLotNo[]> {
    let params = new HttpParams().set('itemID', itemID);
    return this._http.get<itemLotNo[]>(this.url + "/findLotNum", { params: params });

  }

  getDepartmentList(): Observable<IDepartment[]> {
    return this._http.get<IDepartment[]>(this.urlDep);
  }

  getTrxNumFunction(): Observable<string> {
    return this._http.get<string>(this.url + "/getTrxNumFunction");
  }

  getCurrentLocation(ItemID: string, lotNum: string): Observable<string> {
    let params = new HttpParams().set('ItemID', ItemID).set('lotNum', lotNum);
    return this._http.get<string>(this.url + "/GetCurrentLocation", { params: params })
  }

  getRemainingCount(ItemID: string, lotNum: string): Observable<number> {
    let params = new HttpParams().set('ItemID', ItemID).set('lotNum', lotNum);
    return this._http.get<number>(this.url + "/getItemSumCount", { params: params })
  }

  getItemUnit(ItemID: string, lotNum: string): Observable<string> {
    let params = new HttpParams().set('ItemID', ItemID).set('lotNum', lotNum);
    return this._http.get<string>(this.url + "/getItemUnit", { params: params })
  }

  updateLocation(itemTracking: ItemTracking) {
    const headers = new HttpHeaders({
      "Content-Type": "application/json",
      "Accept": "application/json"
    });
    return this._http.post<ItemTracking>(this.url + "/UpdateLocation", JSON.stringify(itemTracking), { headers: headers });

  }

  deleteLocationHistory(trxNum: string): Observable<string> {
    let params = new HttpParams().set('trxNum', trxNum);
    return this._http.delete<string>(this.url + "/DeleteAllTrx", { params: params });
  }
}
