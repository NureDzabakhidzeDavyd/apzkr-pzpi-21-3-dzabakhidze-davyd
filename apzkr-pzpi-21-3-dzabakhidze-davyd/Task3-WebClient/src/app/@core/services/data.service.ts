import { Injectable } from '@angular/core';
import { environment } from "../../../environments/environment";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { BaseEntity } from "../../models";

@Injectable({
  providedIn: 'root'
})
export abstract class DataService<T extends BaseEntity> {
  protected apiUrl = environment.apiUrl;

  constructor(protected http: HttpClient, protected endpoint: string) {
    this.apiUrl = `${this.apiUrl}/api/v1/${endpoint}`;
  }

  getAll(page: number, pageSize: number): Observable<T[]> {
    return this.http.get<T[]>(`${this.apiUrl}?page=${page}&pageSize=${pageSize}`);
  }

  getById(id: string): Observable<T> {
    return this.http.get<T>(`${this.apiUrl}/${id}`);
  }

  create(TData: T): Observable<T> {
    return this.http.post<T>(this.apiUrl, TData);
  }

  delete(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  update(TData: T): Observable<T> {
    return this.http.put<T>(`${this.apiUrl}/${TData.id}`, TData);
  }
}
