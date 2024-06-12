import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BrigadeRescuersRoutingModule } from './brigade-rescuers-routing.module';
import { BrigadeRescuersListComponent } from './components/brigade-rescuers-list/brigade-rescuers-list.component';
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";
import {MatPaginatorModule} from "@angular/material/paginator";
import {MatTableModule} from "@angular/material/table";
import { BrigadeRescuerEditComponent } from './components/brigade-rescuer-edit/brigade-rescuer-edit.component';
import {SharedModule} from "../../@shared/shared.module";
import { BrigadeRescuerDetailsComponent } from './components/brigade-rescuer-details/brigade-rescuer-details.component';


@NgModule({
  declarations: [
    BrigadeRescuersListComponent,
    BrigadeRescuerEditComponent,
    BrigadeRescuerDetailsComponent
  ],
    imports: [
        CommonModule,
        BrigadeRescuersRoutingModule,
        MatButtonModule,
        MatIconModule,
        MatPaginatorModule,
        MatTableModule,
        SharedModule
    ]
})
export class BrigadeRescuersModule { }
