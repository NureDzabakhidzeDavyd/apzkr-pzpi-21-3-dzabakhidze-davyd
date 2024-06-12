import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { VictimsRoutingModule } from './victims-routing.module';
import { VictimsListComponent } from './components/victims-list/victims-list.component';
import { VictimDetailsComponent } from './components/victim-details/victim-details.component';
import {SharedModule} from "../../@shared/shared.module";
import {VictimEditComponent} from "./components/victim-edit-component/victim-edit-component";


@NgModule({
  declarations: [
    VictimsListComponent,
    VictimDetailsComponent,
    VictimEditComponent
  ],
    imports: [
        CommonModule,
        VictimsRoutingModule,
        SharedModule
    ]
})
export class VictimsModule { }
