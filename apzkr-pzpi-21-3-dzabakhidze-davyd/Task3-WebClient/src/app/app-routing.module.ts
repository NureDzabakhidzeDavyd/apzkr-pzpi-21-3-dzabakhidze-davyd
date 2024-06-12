import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {AppHomeComponent} from "./@core/components/app-home/app-home.component";
import {AuthGuard} from "./@core/guards/auth.guard";

const routes: Routes = [
  { path: '', component: AppHomeComponent },
   {path: 'home', redirectTo: '', pathMatch: 'full'},
  { path: 'brigades', loadChildren: () => import('./modules/brigade/brigades.module').then(m => m.BrigadesModule), canActivate: [AuthGuard] },
  { path: 'brigade-rescuers', loadChildren: () => import('./modules/brigade-rescuers/brigade-rescuers.module').then(m => m.BrigadeRescuersModule), canActivate: [AuthGuard] },
  { path: 'diagnoses', loadChildren: () => import('./modules/diagnoses/diagnoses.module').then(m => m.DiagnosesModule), canActivate: [AuthGuard] },
  { path: 'victims', loadChildren: () => import('./modules/victims/victims.module').then(m => m.VictimsModule), canActivate: [AuthGuard] },
  { path: 'actions', loadChildren: () => import('./modules/actions/actions.module').then(m => m.ActionsModule), canActivate: [AuthGuard] },
  { path: 'auth', loadChildren: () => import('./modules/auth/auth.module').then(m => m.AuthModule) },
  { path: 'profile', loadChildren: () => import('./modules/profile/profile.module').then(m => m.ProfileModule), canActivate: [AuthGuard] },
  { path: 'users', loadChildren: () => import('./modules/users/users.module').then(m => m.UsersModule), canActivate: [AuthGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
