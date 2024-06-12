import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BrigadesRoutingModule } from './brigades-routing.module';
import {BrigadeListComponent} from "./components/brigades-list/brigades-list.component";
import {SharedModule} from "../../@shared/shared.module";
import { BrigadeDetailsComponent } from './components/brigade-details/brigade-details.component';
import { BrigadeEditComponent } from './components/brigade-edit/brigade-edit.component';


@NgModule({
  declarations: [
    BrigadeListComponent,
    BrigadeDetailsComponent,
    BrigadeEditComponent,
  ],
  imports: [
    CommonModule,
    BrigadesRoutingModule,
    SharedModule
  ]
})
export class BrigadesModule { }
