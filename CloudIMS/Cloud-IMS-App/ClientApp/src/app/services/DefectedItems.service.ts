import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IDefectedItemsDetailFinal } from '../classes/inventory-management/defected-items/IDefectedItems.interface';
import { IDefectedItemsGetLotNumberFromItemId } from '../classes/inventory-management/defected-items/IDefectedItems.interface';
import { IDefectedItemsGetTransactionNumberFromItemIdLotNumber } from '../classes/inventory-management/defected-items/IDefectedItems.interface';
import { IDefectedItemsGeItemtUnitFromItemID } from '../classes/inventory-management/defected-items/IDefectedItems.interface';
import { IDefectedItemsGeItemtNameFromItemID } from '../classes/inventory-management/defected-items/IDefectedItems.interface';
import { IDefectedItemsToSave } from '../classes/inventory-management/defected-items/IDefectedItems.interface';
import { IDefectedItemsList } from '../classes/inventory-management/defected-items/IDefectedItems.interface';
import { IDefectedItemsFillAll } from '../classes/inventory-management/defected-items/IDefectedItems.interface';
import { IDefectedItemsToSaveClass } from '../classes/inventory-management/defected-items/IDefectedItemsClass';
import { IDefectedItemsRemarks } from '../classes/inventory-management/defected-items/IDefectedItems.interface';
import { IDefectedItemsItemMaster } from '../classes/inventory-management/defected-items/IDefectedItems.interface';

@Injectable({
  providedIn: 'root'
})
export class DefectedItemsService {
  url_get_df_to_list: string = 'api/defecteditems/getDefectedItemsToList'
  url_get_lot_number: string = 'api/defecteditems/getLotNumber'
  url_get_trx_no: string = 'api/defecteditems/getTransactionNumbers'
  url_get_final_details: string = 'api/defecteditems/getFinalResult'
  url_get_name: string = 'api/defecteditems/getName'
  url_get_unit: string = 'api/defecteditems/getUnit'
  url_insert_defect: string = 'api/defecteditems/AddDefectedItem'
  url_auto: string = 'api/defecteditems/getTrxNumFunction'
  url_fill_all: string = 'api/defecteditems/fillAll'
  url_update: string = 'api/defecteditems/updateDefectedItem'
  url_delete: string = 'api/defecteditems/deleteDefectedItem'
  url_get_remarks: string = 'api/defecteditems/getRemarks'
  url_get_items: string = 'api/defecteditems/getItems'

  constructor(private _http: HttpClient) {
  }

  getDfToList(itemName: string): Observable<IDefectedItemsList[]> {
    let params = new HttpParams()
      .set('itemName', itemName);
    return this._http.get<IDefectedItemsList[]>(this.url_get_df_to_list, { params: params });
  }

  getLotNumber(itemId: string): Observable<IDefectedItemsGetLotNumberFromItemId[]> {
    let params = new HttpParams()
      .set('iid', itemId);
    return this._http.get<IDefectedItemsGetLotNumberFromItemId[]>(this.url_get_lot_number, { params: params });
  }

  getTrxNumber(itemId: string, lotno: string, stat: string): Observable<IDefectedItemsGetTransactionNumberFromItemIdLotNumber[]> {
    let params = new HttpParams()
      .set('iid', itemId)
      .set('lotNum', lotno)
      .set('stat', stat);
    return this._http.get<IDefectedItemsGetTransactionNumberFromItemIdLotNumber[]>(this.url_get_trx_no, { params: params });
  }

  getFinalDetails(itemId: string, lotno: string, stat: string): Observable<IDefectedItemsDetailFinal[]> {
    let params = new HttpParams()
      .set('iid', itemId)
      .set('lotNum', lotno)
      .set('stat', stat);
    return this._http.get<IDefectedItemsDetailFinal[]>(this.url_get_final_details, { params: params });
  }

  getSingleName(itemId: string): Observable<string> {
    let params = new HttpParams()
      .set('iid', itemId);
    return this._http.get<string>(this.url_get_name, { params: params });
  }


  getUnit(itemId: string): Observable<IDefectedItemsGeItemtUnitFromItemID[]> {
    let params = new HttpParams()
      .set('iid', itemId);
    return this._http.get<IDefectedItemsGeItemtUnitFromItemID[]>(this.url_get_unit, { params: params });
  }

  addDefectedItems(defetedItemAdd: IDefectedItemsToSaveClass) {
    const headers = new HttpHeaders({
      "Content-Type": "application/json",
      "Accept": "application/json"
    });

    return this._http.post<IDefectedItemsToSaveClass>(this.url_insert_defect, JSON.stringify(defetedItemAdd), { headers: headers })
     

  }

  getTrxNumFunction(): Observable<string> {
    return this._http.get<string>(this.url_auto);
  }

  fillAll(def_trx_no: string, status: string): Observable<IDefectedItemsFillAll[]> {
    let params = new HttpParams()
      .set('def_trx_no', def_trx_no)
      .set('stat',status);
    return this._http.get<IDefectedItemsFillAll[]>(this.url_fill_all, { params: params });
  }

  updateDefectedItems(defectedItems: IDefectedItemsToSaveClass) {
    const headers = new HttpHeaders({
      "Content-Type": "application/json",
      "Accept": "application/json"
    });

    return this._http.post<IDefectedItemsToSaveClass>(this.url_update, JSON.stringify(defectedItems), { headers: headers })


  }


  deleteDefectedItem(def_trx: string): Observable<string> {
    let params = new HttpParams().set('trx_def', def_trx);
    return this._http.delete<string>(this.url_delete, { params: params });

  }

  getRemarks(): Observable<IDefectedItemsRemarks[]> {
    return this._http.get<IDefectedItemsRemarks[]>(this.url_get_remarks);
  }

  getItems(): Observable<IDefectedItemsItemMaster[]> {
    return this._http.get<IDefectedItemsItemMaster[]>(this.url_get_items);
  }




}
