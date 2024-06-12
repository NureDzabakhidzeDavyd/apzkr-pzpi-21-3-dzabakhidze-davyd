import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {ActionsListComponent} from "./components/actions-list/actions-list.component";
import {ActionEditComponent} from "./components/action-edit/action-edit.component";
import {ActionDetailsComponent} from "./components/action-details/action-details.component";

const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'create',
        component: ActionEditComponent
      },
      {
        path: ':id',
        component: ActionDetailsComponent
      },
      {
        path: ':id/edit',
        component: ActionEditComponent
      },
      {
        path: '',
        component: ActionsListComponent
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ActionsRoutingModule { }
