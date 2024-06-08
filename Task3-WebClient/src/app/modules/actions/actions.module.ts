import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ActionsRoutingModule } from './actions-routing.module';
import { ActionEditComponent } from './components/action-edit/action-edit.component';
import {ActionsListComponent} from "./components/actions-list/actions-list.component";
import {SharedModule} from "../../@shared/shared.module";
import { ActionDetailsComponent } from './components/action-details/action-details.component';


@NgModule({
  declarations: [
    ActionEditComponent,
    ActionsListComponent,
    ActionDetailsComponent
  ],
  imports: [
    CommonModule,
    ActionsRoutingModule,
    SharedModule
  ]
})
export class ActionsModule { }
