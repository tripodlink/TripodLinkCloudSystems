import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ISupplier } from '../classes/data-dictionary/Supplier/ISupplier.interface';
import { IUnitCode } from '../classes/data-dictionary/UnitCode/IUnitCode.interface';
import { IiTemMaster } from '../classes/data-dictionary/ItemMaster/IitemMaster.interface';
import { IReportInventoryIn } from '../classes/report-management/report-inventory-in/IReportInventoryIn.interface';

@Injectable()
export class ReportInventoryInService {
  url_rptinvIn: string = 'api/reportinventoryin';
  url_supplier: string = 'api/supplier';
  url_unitcode: string = 'api/unitcode';
  url_itemmaster: string = 'api/itemmaster';


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

  getReportInventoryIn(itemID: string, itemunitID: string, supplierID: string,fromDT: Date, ToDT: Date): Observable<IReportInventoryIn[]> {
    console.log(fromDT.toString());
    let params = new HttpParams().set('ItemID', itemID).set('ItemUNIT', itemunitID).set('supplierID', supplierID).set('fromDT', fromDT.toString()).set('toDT', ToDT.toString());
    return this._http.get<IReportInventoryIn[]>(this.url_rptinvIn + "/GetReportInventoryIN", { params: params })
  }
}
