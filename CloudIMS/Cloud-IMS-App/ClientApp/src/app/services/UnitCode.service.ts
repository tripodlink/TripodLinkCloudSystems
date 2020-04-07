import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IUnitCode } from '../classes/data-dictionary/UnitCode/IUnitCode.interface'
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

@Injectable()
export class  UnitCodeService{
  url: string = 'api/unitcode';

  constructor(private _http: HttpClient) {
    
  }
  
  getUnitCodes(): Observable<IUnitCode[]> {
    return this._http.get<IUnitCode[]>(this.url);
  }

  insertUnitCodes(units: IUnitCode) {
    const headers = new HttpHeaders({
      "Content-Type": "application/json",
      "Accept": "application/json"
    });
    return this._http.post<IUnitCode>("api/unitcode/Add", JSON.stringify(units), { headers: headers });
  }


  updateUnitCodes(itemgroup: IUnitCode) {
    const headers = new HttpHeaders({
      "Content-Type": "application/json",
      "Accept": "application/json"
    });
    return this._http.post<IUnitCode>(this.url + "/Update", JSON.stringify(itemgroup), { headers: headers });

  }

  deleteUnitCodes(id: string): Observable<string> {
    let params = new HttpParams().set("id", id);
    return this._http.delete<string>(this.url + "/Delete", { params: params });

  }

}
