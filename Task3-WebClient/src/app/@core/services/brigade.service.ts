import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Brigade} from "../../models";
import {environment} from "../../../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class BrigadeService {
  private apiUrl = environment.apiUrl + '/api/v1/brigades';

  constructor(private http: HttpClient) {}

  getAll(page: number, pageSize: number): Observable<Brigade[]> {
    return this.http.get<Brigade[]>(`${this.apiUrl}?page=${page}&pageSize=${pageSize}`);
  }

  getById(id: string): Observable<Brigade> {
    return this.http.get<Brigade>(`${this.apiUrl}/${id}`);
  }

  create(brigadeData: any): Observable<Brigade> {
    return this.http.post<Brigade>(this.apiUrl, brigadeData);
  }

  delete(id: string): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  update(BrigadeData: Brigade): Observable<Brigade> {
    return this.http.put<Brigade>(`${this.apiUrl}/${BrigadeData.id}`, BrigadeData);
  }
}
