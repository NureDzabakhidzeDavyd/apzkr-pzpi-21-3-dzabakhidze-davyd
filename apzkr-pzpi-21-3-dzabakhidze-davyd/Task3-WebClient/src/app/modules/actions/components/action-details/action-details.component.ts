import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActionService } from '../../../../@core/services/action.service';
import { Action } from '../../../../models';
import { catchError, of, tap } from 'rxjs';

@Component({
  selector: 'app-action-details',
  templateUrl: './action-details.component.html',
  styleUrls: ['./action-details.component.scss']
})
export class ActionDetailsComponent implements OnInit {
  action?: Action;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private snackBar: MatSnackBar,
    private actionService: ActionService
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const actionId = params.get('id');
      if (actionId) {
        this.loadActionDetails(actionId);
      }
    });
  }

  loadActionDetails(actionId: string): void {
    this.actionService.getById(actionId)
      .pipe(
        tap(action => this.action = action),
        catchError(error => {
          this.snackBar.open('Failed to load action details', 'Close', {
            panelClass: ['custom-snackbar', 'snackbar-error']
          });
          return of(null);
        })
      ).subscribe();
  }

  goBack(): void {
    this.router.navigate(['/actions']);
  }
}
