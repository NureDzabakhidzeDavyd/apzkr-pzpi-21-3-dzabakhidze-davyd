import { Injectable } from '@angular/core';
import {DataService} from "./data.service";
import {BrigadeRescuer, Victim} from "../../models";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class BrigadeRescuerService extends DataService<BrigadeRescuer> {
  constructor(http: HttpClient) {
    super(http, 'brigaderescuers');
  }

  getQRCodeById(id: string): Observable<Blob> {
    return this.http.get(`${this.apiUrl}/${id}/qrcode`, { responseType: 'blob' });
  }
}
