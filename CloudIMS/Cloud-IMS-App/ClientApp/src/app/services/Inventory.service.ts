import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IInventory } from '../classes/IInventory.interface';

@Injectable()
export class InventoryService {
  constructor(private _http: HttpClient) {
  }
  getProgramMenu(): Observable<IInventory[]> {
    return this._http.get<IInventory[]>('api/inventorymanagement');
  }
}
