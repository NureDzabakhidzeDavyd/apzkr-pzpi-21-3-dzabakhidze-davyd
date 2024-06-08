import { Injectable } from '@angular/core';
import {environment} from "../../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {Brigade} from "../../models";

@Injectable({
  providedIn: 'root'
})
export class BackupService {
  private apiUrl = environment.apiUrl + '/api/v1/backup';

  constructor(private http: HttpClient) {}

  getBackup(): Observable<any> {
    return this.http.get(`${this.apiUrl}`, { observe: 'response', responseType: 'blob' });
  }
}
