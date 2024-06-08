import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Victim} from "../../models";
import {DataService} from "./data.service";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class VictimService extends DataService<Victim>{
  constructor(http: HttpClient) {
    super(http, 'victims');
  }

  getQRCodeById(id: string): Observable<Blob> {
    return this.http.get(`${this.apiUrl}/${id}/qrcode`, { responseType: 'blob' });
  }
}

