import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProfileRoutingModule } from './profile-routing.module';
import { ProfileDetailsComponent } from './pages/profile-details/profile-details.component';
import {TranslateModule} from "@ngx-translate/core";


@NgModule({
  declarations: [
    ProfileDetailsComponent
  ],
  imports: [
    CommonModule,
    ProfileRoutingModule,
    TranslateModule
  ]
})
export class ProfileModule { }
