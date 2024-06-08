import { Injectable } from '@angular/core';
import {DataService} from "./data.service";
import {Contact, Victim} from "../../models";
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class UserService  extends DataService<Contact>{
  constructor(http: HttpClient) {
    super(http, 'users');
  }
}
