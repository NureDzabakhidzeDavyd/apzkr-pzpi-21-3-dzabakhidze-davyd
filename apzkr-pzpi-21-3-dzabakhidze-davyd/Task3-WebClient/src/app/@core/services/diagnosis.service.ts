import { Injectable } from '@angular/core';
import {DataService} from "./data.service";
import {Diagnosis} from "../../models";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class DiagnosisService extends DataService<Diagnosis>{
  constructor(http: HttpClient) {
    super(http, 'diagnoses');
  }
}

