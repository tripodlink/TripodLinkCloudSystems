import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IinventoryOutHeader } from '../classes/inventory-management/inventory-out/IinventoryOutHeader.interface';
import { IinventoryOutDetail } from '../classes/inventory-management/inventory-out/IinventoryOutDetail.interface';
import { IDepartment } from '../classes/data-dictionary/Department/IDepartment.interface';
import { IInventoryInTrxDetail } from '../classes/inventory-management/InventoryIn/IInventoryInTrxDetail.interface';
import { IiTemMasterUnitJoinUnit } from '../classes/data-dictionary/ItemMasterUnit/IitemMasterUnitJoinUnit.interface';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

@Injectable()
export class InventorysServices {

  url: string = 'api/inventoryoutheader';

  urldtl: string = 'api/inventoryoutdetail';

  urlDep: string = 'api/department';

  urlInventInDetail: string = 'api/inventoryin';

  urlItmu: string = 'api/itemmasterunit';

  constructor(private _http: HttpClient) {
  }

  getItemGroupData(): Observable<IinventoryOutHeader[]> {
    return this._http.get<IinventoryOutHeader[]>(this.url);
  }

  insertOutHeader(inventoryoutHead: IinventoryOutHeader) {
    const headers = new HttpHeaders({
      "Content-Type": "application/json",
      "Accept": "application/json"
    });
    return this._http.post<IinventoryOutHeader>(this.url + "/Add", JSON.stringify(inventoryoutHead), { headers: headers });

  }

  insertOutDetail(inventoryoutHead: IinventoryOutDetail) {
    const headers = new HttpHeaders({
      "Content-Type": "application/json",
      "Accept": "application/json"
    });
    return this._http.post<IinventoryOutDetail>(this.url + "/Add", JSON.stringify(inventoryoutHead), { headers: headers });

  }

  getDepartmentList(): Observable<IDepartment[]> {
    return this._http.get<IDepartment[]>(this.urlDep);
  }

  getInventInDetail(): Observable<IInventoryInTrxDetail[]> {
    return this._http.get<IInventoryInTrxDetail[]>(this.urlInventInDetail + "/GetInventoryInTrxDetails");
  }

  getJoinUnitAndITMU(id: string): Observable<IiTemMasterUnitJoinUnit[]> {
    let params = new HttpParams().set('id', id);
    return this._http.get<IiTemMasterUnitJoinUnit[]>(this.urlItmu + "/FindID", { params: params });

  }

  getLotNum(itemID: string, unit: string): Observable<IInventoryInTrxDetail[]> {
    let params = new HttpParams().set('itemID', itemID).set('unit', unit);
    return this._http.get<IInventoryInTrxDetail[]>(this.url + "/findLotNum", { params: params });

  }

  findOutHeaderTrxNum(trxNUm: string): Observable<IinventoryOutHeader[]> {
    let params = new HttpParams().set('trxNUm', trxNUm);
    return this._http.get<IinventoryOutHeader[]>(this.url + "/findTrxNum", { params: params });
  }
  //updateItemGroup(itemgroup: IiTemGroup) {
  //  const headers = new HttpHeaders({
  //    "Content-Type": "application/json",
  //    "Accept": "application/json"
  //  });
  //  return this._http.post<IiTemGroup>(this.url + "/Update", JSON.stringify(itemgroup), { headers: headers });

  //}


  //deleteItemGroup(id: string): Observable<string> {
  //  let params = new HttpParams().set("id", id);
  //  return this._http.delete<string>(this.url + "/Delete" , { params: params });

  //}
}
