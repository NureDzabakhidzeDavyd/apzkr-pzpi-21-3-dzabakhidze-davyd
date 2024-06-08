import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DiagnosesRoutingModule } from './diagnoses-routing.module';
import { DiagnosesListComponent } from './components/diagnoses-list/diagnoses-list.component';
import { DiagnosisDetailsComponent } from './components/diagnosis-details/diagnosis-details.component';
import { DiagnosisEditComponent } from './components/diagnosis-edit/diagnosis-edit.component';
import {SharedModule} from "../../@shared/shared.module";


@NgModule({
  declarations: [
    DiagnosesListComponent,
    DiagnosisDetailsComponent,
    DiagnosisEditComponent
  ],
  imports: [
    CommonModule,
    DiagnosesRoutingModule,
    SharedModule
  ]
})
export class DiagnosesModule { }
