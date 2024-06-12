import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthRoutingModule } from './auth-routing.module';
import { LoginComponent } from './login/login.component';
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatIconModule} from "@angular/material/icon";
import {MatInputModule} from "@angular/material/input";
import {ReactiveFormsModule} from "@angular/forms";
import {MatButtonModule} from "@angular/material/button";
import {HttpClientModule} from "@angular/common/http";
import {TranslateModule} from "@ngx-translate/core";
import { RegisterComponent } from './register/register.component';
import {SharedModule} from "../../@shared/shared.module";
import {RouterLink} from "@angular/router";


@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent
  ],
  imports: [
    CommonModule,
    AuthRoutingModule,
    MatFormFieldModule,
    MatInputModule,
    HttpClientModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatIconModule,
    TranslateModule,
    SharedModule,
    RouterLink
  ]
})
export class AuthModule { }
