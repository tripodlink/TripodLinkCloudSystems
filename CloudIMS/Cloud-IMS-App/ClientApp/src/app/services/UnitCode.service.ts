import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IUnitCode } from '../classes/IUnitCode.interface'
import { HttpHeaders } from '@angular/common/http';
import { map, filter, switchMap } from 'rxjs/operators';

@Injectable()
export class  UnitCodeService{
  url: string = 'api/unitcode';

  constructor(private _http: HttpClient) {
    
  }
  
  getUnitCodes(): Observable<IUnitCode[]> {
    return this._http.get<IUnitCode[]>(this.url);
  }

  insertUnitCodes(units: IUnitCode[]) {
    const headers = new HttpHeaders({
      "Content-Type": "application/json",
      "Accept": "application/json"
    });
    return this._http.post<IUnitCode[]>("api/unitcode/Add", JSON.stringify(units), { headers: headers });
    
    
  }
}
