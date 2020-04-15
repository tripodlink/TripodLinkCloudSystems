import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IinventoryOutHeader } from '../classes/inventory-management/inventory-out/IitemGroup.interface';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
@Injectable()
export class InventoryOutHeaderServices {

  url: string = 'api/inventoryoutheader';

  constructor(private _http: HttpClient) {
  }

  getItemGroupData(): Observable<IinventoryOutHeader[]> {
    return this._http.get<IinventoryOutHeader[]>(this.url);
  }

  insertItemGroup(inventoryoutHead: IinventoryOutHeader) {
    const headers = new HttpHeaders({
      "Content-Type": "application/json",
      "Accept": "application/json"
    });
    return this._http.post<IinventoryOutHeader>(this.url + "/Add", JSON.stringify(inventoryoutHead), { headers: headers });

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
