import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ISupplier } from '../classes/data-dictionary/Supplier/ISupplier.interface';

@Injectable()
export class SupplierService {
  url: string = 'api/supplier';
  constructor(private _http: HttpClient) {
  }
  getSupplier(): Observable<ISupplier[]> {
    return this._http.get<ISupplier[]>(this.url);
  }

  insertSupplier(supplier: ISupplier) {
    const headers = new HttpHeaders({
      "Content-Type": "application/json",
      "Accept": "application/json"
    });
    return this._http.post<ISupplier>(this.url+"/Add", JSON.stringify(supplier), { headers: headers });
  }
  updateSupplier(supplier: ISupplier) {
    const headers = new HttpHeaders({
      "Content-Type": "application/json",
      "Accept": "application/json"
    });
    return this._http.post<ISupplier>(this.url + "/Update", JSON.stringify(supplier), { headers: headers });

  }

  deleteSupplier(id: string):Observable<string> {
    let params = new HttpParams().set('id', id.toString());
    return this._http.delete<string>(this.url+"/Delete", {params:params});
  }
}
