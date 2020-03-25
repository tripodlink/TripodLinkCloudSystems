import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ISupplier } from '../classes/ISupplier.interface';

@Injectable()
export class SupplierService {

  constructor(private _http: HttpClient) {
  }
  getSupplier(): Observable<ISupplier[]> {
    return this._http.get<ISupplier[]>('api/supplier');
  }
}
