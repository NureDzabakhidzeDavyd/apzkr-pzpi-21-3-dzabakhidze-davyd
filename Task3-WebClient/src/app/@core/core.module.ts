import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {AppShellComponent} from "./components/app-shell/app-shell.component";
import {MatSidenavModule} from "@angular/material/sidenav";
import {MatIconModule} from "@angular/material/icon";
import {MatListModule} from "@angular/material/list";
import {RouterLink, RouterOutlet} from "@angular/router";
import {MatToolbarModule} from "@angular/material/toolbar";
import {AppHomeComponent} from "./components/app-home/app-home.component";
import {HttpClientModule} from "@angular/common/http";
import {MatDialogModule} from "@angular/material/dialog";
import {SharedModule} from "../@shared/shared.module";

@NgModule({
  declarations: [AppShellComponent, AppHomeComponent],
  exports: [
    AppShellComponent
  ],
    imports: [
        CommonModule,
        MatSidenavModule,
        MatIconModule,
        MatListModule,
        RouterOutlet,
        MatToolbarModule,
        RouterLink,
        HttpClientModule,
        MatDialogModule,
        SharedModule,
    ]
})
export class CoreModule { }
