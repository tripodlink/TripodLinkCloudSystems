import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IInventoryInTrxHeader } from '../classes/inventory-management/InventoryIn/IInventoryInTrxHeader.interface';
import { IInventoryInTrxHeaderClass } from '../classes/inventory-management/InventoryIn/IInventoryInTrxHeaderClass';
import { ISupplier } from '../classes/data-dictionary/Supplier/ISupplier.interface';
import { IUnitCode } from '../classes/data-dictionary/UnitCode/IUnitCode.interface';
import { IiTemMaster } from '../classes/data-dictionary/ItemMaster/IitemMaster.interface';
import { IInventoryInTrxDetailClass } from '../classes/inventory-management/InventoryIn/IInventoryInTrxDetailClass';
import { IiTemMasterUnit } from '../classes/data-dictionary/ItemMasterUnit/IitemMasterUnit.interface';
import { Http } from '@angular/http';
import { IiTemMasterUnitJoinUnit } from '../classes/data-dictionary/ItemMasterUnit/IitemMasterUnitJoinUnit.interface';
import { IitemMasterUnitJoinUnitClass } from '../classes/data-dictionary/ItemMasterUnit/IitemMasterJoinUnitClass';
import { IitemMasterUnitConversionFactor } from '../classes/data-dictionary/ItemMasterUnit/IitemMasterUnitConversionFactor.interface';
import { IItemListStockIn } from '../classes/home/IItemListStockIn.interface';
import { IInventoryInTrxList, IInventoryInTrxDetail } from '../classes/inventory-management/InventoryIn/IInventoryInTrxDetail.interface';

@Injectable()
export class InventoryInService {
  url_invIn: string = 'api/inventoryin';
  url_supplier: string = 'api/supplier';
  url_unitcode: string = 'api/unitcode';
  url_itemmaster: string = 'api/itemmaster';
  url_GetListOfTrxListInvIn: string = 'api/inventoryin/GetTrxListInventoryIn';
  url_UpdateTrxListInvInHdr: string = 'api/inventoryin/Update_Trx_Header';
  url_UpdateTrxListInvInDtl: string = 'api/inventoryin/Update_Trx_Detail';
  url_DeleteTrxListInvInHdr: string = 'api/inventoryin/Delete_Trx_Header';
  url_DeleteTrxListInvInDtl: string = 'api/inventoryin/Delete_Trx_Detail';


  constructor(private _http: HttpClient) {
  }
  getSupplierData(): Observable<ISupplier[]> {
    return this._http.get<ISupplier[]>(this.url_supplier);
  }
    getUnitCodeData(): Observable<IUnitCode[]> {
      return this._http.get<IUnitCode[]>(this.url_unitcode);
  }
    getItemMasterData(): Observable<IiTemMaster[]> {
      return this._http.get<IiTemMaster[]>(this.url_itemmaster);
  }

  insertInventoryInTrxHeader(trxheader: IInventoryInTrxHeaderClass) {
    const headers = new HttpHeaders({
      "Content-Type": "application/json",
      "Accept": "application/json"
    });
    return this._http.post<IInventoryInTrxHeaderClass[]>(this.url_invIn + "/Add_Trx_Header", JSON.stringify(trxheader), { headers: headers });

  }


  insertInventoryInTrxDetails(trxdetails: IInventoryInTrxDetailClass) {
    const headers = new HttpHeaders({
      "Content-Type": "application/json",
      "Accept": "application/json"
    });
    return this._http.post<IInventoryInTrxDetailClass[]>(this.url_invIn + "/Add_Trx_Detail", JSON.stringify(trxdetails), { headers: headers });

  }

  updateInventoryInTrxHeader(trxheader: IInventoryInTrxHeader) {
    const headers = new HttpHeaders({
      "Content-Type": "application/json",
      "Accept": "application/json"
    });
    return this._http.post<IInventoryInTrxHeader>(this.url_UpdateTrxListInvInHdr, JSON.stringify(trxheader), { headers: headers });

  }

  updateInventoryInTrxDetails(trxdetails: IInventoryInTrxDetail) {
    const headers = new HttpHeaders({
      "Content-Type": "application/json",
      "Accept": "application/json"
    });
    return this._http.post<IInventoryInTrxDetail>(this.url_UpdateTrxListInvInDtl, JSON.stringify(trxdetails), { headers: headers });

  }


  deleteInventoryInTrxDetails(trxno: string): Observable<string> {
    let params = new HttpParams().set('trxno', trxno.toString());
    return this._http.delete<string>(this.url_DeleteTrxListInvInDtl, { params: params });
  }

  deleteInventoryInTrxHeader(trxno: string): Observable<string> {
    let params = new HttpParams().set('trxno', trxno.toString());
    return this._http.delete<string>(this.url_DeleteTrxListInvInHdr, { params: params });
  }

  getItemasterUnit(id: string): Observable<IitemMasterUnitJoinUnitClass[]> {
    let params = new HttpParams().set('id', id);
    return this._http.get<IitemMasterUnitJoinUnitClass[]>(this.url_invIn + "/GetUnitCodeFromItem", {params : params})
  }

  getItemasterUnitConversionFactor(itemmasterId: string, itemmasterUnitID: string): Observable<IitemMasterUnitConversionFactor[]> {
    let params = new HttpParams().set('id', itemmasterId).set('idunit', itemmasterUnitID)
    return this._http.get<IitemMasterUnitConversionFactor[]>(this.url_invIn + "/GetConversionFactorFromItemMasterUnit", { params: params })
  }

  getTrxNumFunction(): Observable<string> {
    return this._http.get<string>(this.url_invIn + "/getTrxNumFunction");
  }


  getTrxListInvIn(): Observable<IInventoryInTrxList[]> {
    return this._http.get<IInventoryInTrxList[]>(this.url_GetListOfTrxListInvIn);
  }
}
