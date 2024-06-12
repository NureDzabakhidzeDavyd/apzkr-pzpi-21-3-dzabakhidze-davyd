import { Component } from '@angular/core';
import {animate, style, transition, trigger} from "@angular/animations";

@Component({
  selector: 'app-home',
  templateUrl: './app-home.component.html',
  styleUrls: ['./app-home.component.scss'],
  animations: [
    trigger('fadeInOut', [
      transition(':enter', [
        style({ opacity: 0 }),
        animate('500ms', style({ opacity: 1 })),
      ]),
    ]),
  ],
})
export class AppHomeComponent {

}
