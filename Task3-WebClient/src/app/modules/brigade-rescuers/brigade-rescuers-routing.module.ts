import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {BrigadeRescuersListComponent} from "./components/brigade-rescuers-list/brigade-rescuers-list.component";
import {BrigadeRescuerEditComponent} from "./components/brigade-rescuer-edit/brigade-rescuer-edit.component";
import {BrigadeRescuerDetailsComponent} from "./components/brigade-rescuer-details/brigade-rescuer-details.component";

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'create',
        component: BrigadeRescuerEditComponent
      },
      {
        path: ':id',
        component: BrigadeRescuerDetailsComponent
      },
      {
        path: ':id/edit',
        component: BrigadeRescuerEditComponent
      },
      {
        path: '',
        component: BrigadeRescuersListComponent
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BrigadeRescuersRoutingModule { }
