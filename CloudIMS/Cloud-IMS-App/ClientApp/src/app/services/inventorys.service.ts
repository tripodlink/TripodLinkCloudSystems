import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IinventoryOutHeader } from '../classes/inventory-management/inventory-out/IinventoryOutHeader.interface';
import { IinventoryOutFindTrx } from '../classes/inventory-management/inventory-out/IinventoryOutHeader.interface';
import { IinventoryOutDetail } from '../classes/inventory-management/inventory-out/IinventoryOutDetail.interface';
import { IinventoryOutTable } from '../classes/inventory-management/inventory-out/IinventoryOutDetail.interface';
import { IDepartment } from '../classes/data-dictionary/Department/IDepartment.interface';
import { IInventoryInTrxDetail } from '../classes/inventory-management/InventoryIn/IInventoryInTrxDetail.interface';
import { IiTemMasterUnitJoinUnit } from '../classes/data-dictionary/ItemMasterUnit/IitemMasterUnitJoinUnit.interface';
import { IitemMasterJoinInvIN } from '../classes/data-dictionary/ItemMasterUnit/IitemMasterJoinInvIN.interface';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { IInventoryInTrxHeader } from '../classes/inventory-management/InventoryIn/IInventoryInTrxHeader.interface';
import { itemLotNo, ItemTracking } from '../classes/inventory-management/itemTracking/itemTracking.interface';
@Injectable()
export class InventorysServices {

  url: string = 'api/inventoryoutheader';

  urldtl: string = 'api/inventoryoutdetail';

  urlDep: string = 'api/department';

  urlInventInDetail: string = 'api/inventoryin';

  urlItmu: string = 'api/itemmasterunit';

  urlTracking: string = 'api/itemtracking';
  constructor(private _http: HttpClient) {
  }


  //INSERT
  insertOutHeader(inventoryoutHead: IinventoryOutHeader) {
    const headers = new HttpHeaders({
      "Content-Type": "application/json",
      "Accept": "application/json"
    });
    return this._http.post<IinventoryOutHeader>(this.url + "/Add", JSON.stringify(inventoryoutHead), { headers: headers });

  }

  insertOutDetail(inventoryouDetail: IinventoryOutDetail) {
    const headers = new HttpHeaders({
      "Content-Type": "application/json",
      "Accept": "application/json"
    });
    return this._http.post<IinventoryOutDetail>(this.urldtl + "/Add", JSON.stringify(inventoryouDetail), { headers: headers });

  }
  //


  //GET

  getItemGroupData(): Observable<IinventoryOutHeader[]> {
    return this._http.get<IinventoryOutHeader[]>(this.url);
  }

  getDepartmentList(): Observable<IDepartment[]> {
    return this._http.get<IDepartment[]>(this.urlDep);
  }

  getIfApprover(userID: string): Observable<boolean> {
    let params = new HttpParams().set('userID', userID);
    return this._http.get<boolean>(this.url + "/GetIfApprover", { params: params });

  }

  getJoinUnitAndITMU(id: string): Observable<IiTemMasterUnitJoinUnit[]> {
    let params = new HttpParams().set('id', id);
    return this._http.get<IiTemMasterUnitJoinUnit[]>(this.urlItmu + "/FindID", { params: params });

  }

  getInventInDetail(): Observable<IInventoryInTrxDetail[]> {
    return this._http.get<IInventoryInTrxDetail[]>(this.urldtl + "/JoinINVtoItemMaster");
  }

  getLotNum(itemID: string, unit: string): Observable<IitemMasterJoinInvIN[]> {
    let params = new HttpParams().set('itemID', itemID).set('unit', unit);
    return this._http.get<IitemMasterJoinInvIN[]>(this.url + "/findLotNum", { params: params });

  }

  getRemainingCount(trxNum: string,itemID: string, lotNum: string): Observable<IInventoryInTrxDetail[]> {
    let params = new HttpParams().set("trxID", trxNum).set('itemID', itemID).set('lotNum', lotNum);
    return this._http.get<IInventoryInTrxDetail[]>(this.urldtl + "/findRemainingCount", { params: params });
  }

  getTrxTable(trxNum: string): Observable<IinventoryOutTable[]> {
    let params = new HttpParams().set('trxNum', trxNum);
    return this._http.get<IinventoryOutTable[]>(this.urldtl + "/getAllTrx", { params: params });
  }

  getTrxNumFunction(): Observable<string> {
    return this._http.get<string>(this.url + "/getTrxNumFunction");
  }

 

  searchTrx(type:string, dateFrom: Date, dateTo: Date): Observable<IinventoryOutFindTrx[]> {
    let params = new HttpParams().set('type', type).set('dateFrom', dateFrom.toString()).set('dateTo', dateTo.toString());
    return this._http.get<IinventoryOutFindTrx[]>(this.url + "/searchTrx", {params: params});
  }


  getTrxNumFunctionTracking(): Observable<string> {
    return this._http.get<string>(this.urlTracking + "/getTrxNumFunction");
  }
  //

  //FIND
  findOutHeaderTrxNum(trxNUm: string): Observable<IinventoryOutHeader[]> {
    let params = new HttpParams().set('trxNUm', trxNUm);
    return this._http.get<IinventoryOutHeader[]>(this.url + "/findTrxNum", { params: params });
  }

  findPendingTrx(): Observable<IinventoryOutHeader[]> {
    return this._http.get<IinventoryOutHeader[]>(this.url + "/findPendingTrx");
  }
 
  //

  //DELETE
  deleteAllDetails(trxNum: string): Observable<string> {
    let params = new HttpParams().set('trxNum', trxNum);
    return this._http.delete<string>(this.urldtl + "/DeleteAll", { params: params });
  }

  deleteAllHeader(trxNum: string): Observable<string> {
    let params = new HttpParams().set('trxNum', trxNum);
    return this._http.delete<string>(this.url + "/DeleteAllTrx", { params: params });
  }
  //

  //UPDATE
  updatePendingTrx(invHead: IinventoryOutHeader) {
    let headers = new HttpHeaders({
      "Content-Type": "application/json",
      "Accept": "application/json"
    });
    return this._http.post<IinventoryOutHeader>(this.url + "/UpdatePendingTrx", JSON.stringify(invHead), { headers: headers });
  }

  updateRemaningCount(trxNum: string, minCount: string): Observable<string> {
    let params = new HttpParams().set('trxNum', trxNum).set('minCount', minCount);
    return this._http.get<string>(this.urldtl + "/UpdateINVInRemainingCount", { params: params });
  }


  updateLocation(itemTracking: ItemTracking) {
    const headers = new HttpHeaders({
      "Content-Type": "application/json",
      "Accept": "application/json"
    });
    return this._http.post<ItemTracking>(this.urlTracking + "/UpdateLocation", JSON.stringify(itemTracking), { headers: headers });

  }
  
}
