import {Component, ViewChild} from '@angular/core';
import {MatSidenav} from "@angular/material/sidenav";
import {BreakpointObserver} from "@angular/cdk/layout";
import {TranslateService} from "@ngx-translate/core";

@Component({
  selector: 'app-shell',
  templateUrl: './app-shell.component.html',
  styleUrls: ['./app-shell.component.scss']
})
export class AppShellComponent {
  @ViewChild(MatSidenav)
  sidenav!: MatSidenav;
  isCollapsed = true;
  isMobile= true;

  constructor(private observer: BreakpointObserver, public translate: TranslateService) {}

  ngOnInit() {
    this.observer.observe(['(max-width: 800px)']).subscribe((screenSize) => {
      this.isMobile = screenSize.matches;
    });
  }

  toggleMenu() {
    if(this.isMobile){
      this.sidenav.toggle();
      this.isCollapsed = false; // On mobile, the menu can never be collapsed
    } else {
      this.sidenav.open(); // On desktop/tablet, the menu can never be fully closed
      this.isCollapsed = !this.isCollapsed;
    }
  }

  switchLanguage() {
    const currentLang = this.translate.currentLang;
    const newLang = currentLang === 'en' ? 'ua' : 'en';
    this.translate.use(newLang);
  }
}
