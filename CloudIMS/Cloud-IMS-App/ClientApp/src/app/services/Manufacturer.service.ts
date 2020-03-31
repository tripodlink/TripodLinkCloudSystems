import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IManufacturer } from '../classes/data-dictionary/Manufacturer/IManufacturer.interface';

@Injectable()
export class ManufacturerService {
  url: string = 'api/manufacturer';
  constructor(private _http: HttpClient) {
  }
  getManufacturer(): Observable<IManufacturer[]> {
    return this._http.get<IManufacturer[]>(this.url);
  }

  insertManufacturer(manufact: IManufacturer) {
    const headers = new HttpHeaders({
      "Content-Type": "application/json",
      "Accept": "application/json"
    });
    return this._http.post<IManufacturer>(this.url + "/Add", JSON.stringify(manufact), { headers: headers });
  }
  updateManufacturer(manufact: IManufacturer) {
    const headers = new HttpHeaders({
      "Content-Type": "application/json",
      "Accept": "application/json"
    });
    return this._http.post<IManufacturer>(this.url + "/Update", JSON.stringify(manufact), { headers: headers });

  }

  deleteManufacturer(id: string):Observable<string> {
    let params = new HttpParams().set('id', id.toString());
    return this._http.delete<string>(this.url+"/Delete", {params:params});
  }
}
