import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { forkJoin, Observable } from 'rxjs';
import { DataInfo } from './data-info';
import { DataFilter } from './data-filter'
import { DataModel } from './data-model';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor(private http: HttpClient) { }

  getDataInfo(): Observable<DataInfo> {
    return this.http.get<DataInfo>("/api/data/info");
  }

  getDataEC(Filter: DataFilter) {
    return this.http.post<DataModel[]>('/api/data/ecommerce/filter', Filter);
  }
  getDataIU(Filter: DataFilter) {
    return this.http.post<DataModel[]>('/api/data/internetuse/filter', Filter);
  }

}
