import { Injectable, StreamingResourceOptions, inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { RelationsResponse } from '../models/relation.model'; 

@Injectable({
  providedIn: 'root',
})
export class RelationService {
  #http = inject(HttpClient);

  private accessToken: string = '';
  private relationsListSubject = new BehaviorSubject<RelationsResponse | null>(null);
  relationsList$ = this.relationsListSubject.asObservable();

  #getTokensFromLocalStorage() {
    this.accessToken = localStorage.getItem('accessToken') ?? '';
  }

getData(Name: string): Observable<RelationsResponse> {
  this.#getTokensFromLocalStorage();
  const params = {
    searchTerm: Name,
    page: 0,
    pageSize: 1,
    sortByColumn: 'Name',
    sortDirection: 'Ascending',
    filters: {
      alphaNumeric: [{ columnName: 'IsActive', type: 'EqualTo', value: 'True' }],
      numeric: [{ columnName: 'Code', type: 'GreaterThanOrEqualTo', value: 2 }],
      date: [],
    },
  };

  const headers = new HttpHeaders({
    Authorization: `Bearer ${this.accessToken}`,
    'Content-Type': 'application/json',
  });

  return this.#http.post<RelationsResponse>(
    '',
    params,
    { headers }
  );
}

}
