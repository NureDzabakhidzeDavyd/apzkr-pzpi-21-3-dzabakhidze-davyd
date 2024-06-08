import {RouterModule, Routes} from "@angular/router";
import {NgModule} from "@angular/core";
import {UsersListComponent} from "./components/users-list/users-list.component";

const routes: Routes = [
  {
    path: '',
    children: [
      // {
      //   path: 'create',
      //   component: VictimEditComponent
      // },
      // {
      //   path: ':id',
      //   component: VictimDetailsComponent
      // },
      // {
      //   path: ':id/edit',
      //   component: VictimEditComponent
      // },
      {
        path: '',
        component: UsersListComponent
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsersRoutingModule { }
