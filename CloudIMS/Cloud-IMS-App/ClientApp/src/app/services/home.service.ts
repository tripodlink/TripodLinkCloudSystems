import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IHome } from '../classes/home/IHome.interface';
import { ICountUser } from '../classes/home/ICountUser.interface';
import { ICountStockIn } from '../classes/home/ICountStockIn.interface';
import { ICountStockOut } from '../classes/home/ICountStockOut.interface';
import { IItemListStockIn } from '../classes/home/IItemListStockIn.interface';
import { IItemListStockOut } from '../classes/home/IItemListStockOut.interface';
import { IItemListPendingToStockOut } from '../classes/home/IItemListPendingToStockOut.interface';

import { ICountNotif } from '../classes/app-sidebar-menu/ICountNotif.interface';

@Injectable()
export class HomeService {

  url_count_user: string = 'api/home/CountUser'
  url_count_stockIn: string = 'api/home/Count_StockIn';
  url_count_stockOut: string = 'api/home/Count_StockOut';
  url_GetListOfItemStockIn: string = 'api/home/GetListOfItemStockIn';
  url_GetListOfItemStockOut: string = 'api/home/GetListOfItemStockOut';
  url_GetListOfItemPending_To_StockOut: string = 'api/home/GetListOfItemPending_To_StockOut';
  url_count_notif: string = 'api/home/CountNotif';

  
  constructor(private _http: HttpClient) {
  }
  getCountUser(): Observable<ICountUser[]> {
    return this._http.get<ICountUser[]>(this.url_count_user);
  }
  getCountStockIn(): Observable<ICountStockIn[]> {
    return this._http.get<ICountStockIn[]>(this.url_count_stockIn);
  }
  getCountStockOut(): Observable<ICountStockOut[]> {
    return this._http.get<ICountStockOut[]>(this.url_count_stockOut);
  }
  getItemListStockIn(): Observable<IItemListStockIn[]> {
    return this._http.get<IItemListStockIn[]>(this.url_GetListOfItemStockIn);
  }
  getItemListStockOut(): Observable<IItemListStockOut[]> {
    return this._http.get<IItemListStockOut[]>(this.url_GetListOfItemStockOut);
  }
  getItemListPending_To_StockOut(): Observable<IItemListPendingToStockOut[]> {
    return this._http.get<IItemListPendingToStockOut[]>(this.url_GetListOfItemPending_To_StockOut);
  }
  getCountNotif(): Observable<ICountNotif[]> {
    return this._http.get<ICountNotif[]>(this.url_count_notif);
  }
}
