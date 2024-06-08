import { Injectable } from '@angular/core';
import {DataService} from "./data.service";
import {Action} from "../../models";
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class ActionService  extends DataService<Action>{
  constructor(http: HttpClient) {
    super(http, 'actions');
  }
}

