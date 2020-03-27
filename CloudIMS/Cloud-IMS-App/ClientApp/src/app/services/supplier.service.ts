import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ISupplier } from '../classes/ISupplier.interface';

@Injectable()
export class SupplierService {

  constructor(private _http: HttpClient) {
  }
  getSupplier(): Observable<ISupplier[]> {
    return this._http.get<ISupplier[]>('api/supplier');
  }

  insertSupplier(supplier: ISupplier[]) {
    const headers = new HttpHeaders({
      "Content-Type": "application/json",
      "Accept": "application/json"
    });
    return this._http.post<ISupplier[]>("api/supplier/Add", JSON.stringify(supplier), { headers: headers });


  }

  deleteSupplier(id: string):Observable<string> {
    let params = new HttpParams().set('id', id);
    return this._http.delete<string>("api/supplier/DeleteSupplier", {params:params});
  }
}
