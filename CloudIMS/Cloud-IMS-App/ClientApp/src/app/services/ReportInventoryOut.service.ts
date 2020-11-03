import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IReportInventoryOut} from '../classes/report-management/report-inventory-out/IReportInventoryOut.interface';
import {IReportTally } from '../classes/report-management/report-tally/IReportTally.interface';
import { IiTemMaster } from '../classes/data-dictionary/ItemMaster/IitemMaster.interface';
import { IUnitCode } from '../classes/data-dictionary/UnitCode/IUnitCode.interface';
import { IDepartment } from '../classes/data-dictionary/Department/IDepartment.interface';

@Injectable()
export class ReportInventoryOutService {
  url: string = 'api/reportinventoryout';
  urlItemID = 'api/itemmaster';
  urlUnit = 'api/unitcode';
  urlDep = 'api/department';

  constructor(private _http: HttpClient) {

  }
  getReport(trxNum: string, itemName: string, itemUnit: string, department: string, dateFrom: Date, dateTo: Date,
  reportType: string): Observable<IReportInventoryOut[]> {
    let params = new HttpParams()
      .set('trxNum', trxNum)
      .set('itemName', itemName)
      .set('itemUnit', itemUnit)
      .set('department', department)
      .set('dateFrom', dateFrom.toString())
      .set('dateTo', dateTo.toString())
      .set('reportType', reportType);
    return this._http.get<IReportInventoryOut[]>(this.url + "/FindReport", { params: params });
  }

  getItem(): Observable<IiTemMaster[]> {
    return this._http.get<IiTemMaster[]>(this.urlItemID);
  }

  getUnit(): Observable<IUnitCode[]> {
    return this._http.get<IUnitCode[]>(this.urlUnit);
  }

  getDepartment(): Observable<IDepartment[]> {
    return this._http.get<IDepartment[]>(this.urlDep);
  }

  getTallyReport(): Observable<IReportTally[]> {
    return this._http.get<IReportTally[]>(this.url + "/GenerateTallyReport");
  }

  getItemTotalCount(itemID: string, lotNum:string): Observable<number> {
    let params = new HttpParams().set('itemID', itemID).set('lotNum', lotNum);
    return this._http.get<number>(this.url + "/GetItemTotalCount", { params: params})
  }

  getItemInventoryIn(itemID: string, lotNum: string): Observable<number> {
    let params = new HttpParams().set('itemID', itemID).set('lotNum', lotNum);
    return this._http.get<number>(this.url + "/GetItemInventoryIn", { params: params})
  }

  getItemDefect(itemID: string, lotNum: string): Observable<number> {
    let params = new HttpParams().set('itemID', itemID).set('lotNum', lotNum);
    return this._http.get<number>(this.url + "/GetItemDefect", { params: params})
  }

  getItemInventoryOut(itemID: string, InTrxNo: string): Observable<number> {
    let params = new HttpParams().set('itemID', itemID).set('InTrxNo', InTrxNo);
    return this._http.get<number>(this.url + "/GetItemInventoryOut", { params: params})
  }
}
