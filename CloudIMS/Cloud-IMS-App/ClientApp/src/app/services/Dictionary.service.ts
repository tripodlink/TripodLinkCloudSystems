import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IDictionary } from '../classes/data-dictionary/IDictionary.interface';

@Injectable()
export class DictionaryService {
  constructor(private _http: HttpClient) {
  }
  getProgramMenu(): Observable<IDictionary[]> {
    return this._http.get<IDictionary[]>('api/dictionary');
  }
}
