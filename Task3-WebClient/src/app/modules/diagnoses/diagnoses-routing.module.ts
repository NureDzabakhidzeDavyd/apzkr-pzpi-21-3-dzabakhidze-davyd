import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {DiagnosisDetailsComponent} from "./components/diagnosis-details/diagnosis-details.component";
import {DiagnosisEditComponent} from "./components/diagnosis-edit/diagnosis-edit.component";
import {DiagnosesListComponent} from "./components/diagnoses-list/diagnoses-list.component";

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: ':id',
        component: DiagnosisDetailsComponent
      },
      {
        path: ':id/edit',
        component: DiagnosisEditComponent
      },
      {
        path: 'create',
        component: DiagnosisEditComponent
      },
      {
        path: '',
        component: DiagnosesListComponent
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DiagnosesRoutingModule { }
