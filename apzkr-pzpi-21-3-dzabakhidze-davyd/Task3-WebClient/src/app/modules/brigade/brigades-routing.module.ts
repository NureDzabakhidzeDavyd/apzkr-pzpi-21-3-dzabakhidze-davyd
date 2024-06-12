import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {BrigadeListComponent} from "./components/brigades-list/brigades-list.component";
import {BrigadeDetailsComponent} from "./components/brigade-details/brigade-details.component";
import {BrigadeEditComponent} from "./components/brigade-edit/brigade-edit.component";

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'create',
        component: BrigadeEditComponent
      },
      {
        path: ':id',
        component: BrigadeDetailsComponent
      },
      {
        path: ':id/edit',
        component: BrigadeEditComponent
      },
      {
        path: '',
        component: BrigadeListComponent
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BrigadesRoutingModule { }
