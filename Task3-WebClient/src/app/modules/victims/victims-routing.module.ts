import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {VictimsListComponent} from "./components/victims-list/victims-list.component";
import {VictimDetailsComponent} from "./components/victim-details/victim-details.component";
import {VictimEditComponent} from "./components/victim-edit-component/victim-edit-component";

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'create',
        component: VictimEditComponent
      },
      {
        path: ':id',
        component: VictimDetailsComponent
      },
      {
        path: ':id/edit',
        component: VictimEditComponent
      },
      {
        path: '',
        component: VictimsListComponent
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class VictimsRoutingModule { }
