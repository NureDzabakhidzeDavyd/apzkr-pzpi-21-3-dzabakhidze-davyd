import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {SharedModule} from "../../@shared/shared.module";
import {UsersRoutingModule} from "./users-routing.module";
import { UsersListComponent } from './components/users-list/users-list.component';



@NgModule({
  declarations: [
    UsersListComponent
  ],
  imports: [
    CommonModule,
    UsersRoutingModule,
    SharedModule
  ]
})
export class UsersModule { }
