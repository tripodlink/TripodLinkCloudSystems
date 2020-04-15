import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IDepartment } from '../classes/data-dictionary/Department/IDepartment.interface';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
@Injectable()
export class DepartmentServices {

  url: string = 'api/department';

  constructor(private _http: HttpClient) {
  }

  getDepartmentList(): Observable<IDepartment[]> {
    return this._http.get<IDepartment[]>(this.url);
  }

  insertDepartment(deptList: IDepartment) {
    const headers = new HttpHeaders({
      "Content-Type": "application/json",
      "Accept": "application/json"
    });
    return this._http.post<IDepartment>(this.url + "/Add", JSON.stringify(deptList), { headers: headers });

  }

  updateDepartment(deptList: IDepartment) {
    const headers = new HttpHeaders({
      "Content-Type": "application/json",
      "Accept": "application/json"
    });
    return this._http.post<IDepartment>(this.url + "/Update", JSON.stringify(deptList), { headers: headers });

  }


  deleteItemGroup(id: string): Observable<string> {
    let params = new HttpParams().set("id", id);
    return this._http.delete<string>(this.url + "/Delete" , { params: params });

  }
}
