import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActionService } from '../../../../@core/services/action.service';
import { BrigadeRescuerService } from '../../../../@core/services/brigade-rescuer.service';
import { VictimService } from '../../../../@core/services/victim.service';
import { BrigadeRescuer } from '../../../../models';
import { Victim } from '../../../../models';
import { catchError, tap, of } from 'rxjs';

@Component({
  selector: 'app-action-edit',
  templateUrl: './action-edit.component.html',
  styleUrls: ['./action-edit.component.scss']
})
export class ActionEditComponent implements OnInit {
  actionForm: FormGroup;
  availableRescuers: BrigadeRescuer[] = [];
  availableVictims: Victim[] = [];
  actionId?: string;
  page: number = 0;
  pageSize: number = 10;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private snackBar: MatSnackBar,
    private actionService: ActionService,
    private brigadeRescuerService: BrigadeRescuerService,
    private victimService: VictimService
  ) {
    this.actionForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      actionTime: ['', Validators.required],
      actionType: ['', Validators.required],
      actionPlace: ['', Validators.required],
      brigadeRescuerId: ['', Validators.required],
      victimId: ['', Validators.required],
    });
  }

  ngOnInit(): void {
      console.log('test: ', this.actionId);
    this.loadMoreRescuers();
    this.loadMoreVictims();

    this.route.paramMap.subscribe(params => {
      this.actionId = params.get('id') ?? undefined;
      if (this.actionId) {
        this.loadAction();
      }
    });
  }

  loadMoreRescuers(): void {
    this.brigadeRescuerService.getAll(this.page, this.pageSize)
      .pipe(
        tap(rescuers => {
          this.availableRescuers = [...this.availableRescuers, ...rescuers];
          this.page++;
        })
      ).subscribe();
  }

  loadMoreVictims(): void {
    this.victimService.getAll(this.page, this.pageSize)
      .pipe(
        tap(victims => {
          this.availableVictims = [...this.availableVictims, ...victims];
          this.page++;
        })
      ).subscribe();
  }

  loadAction(): void {
    if (this.actionId) {
      this.actionService.getById(this.actionId)
        .pipe(
          tap(action => {
            if (action) {
              this.actionForm.patchValue(action);
            }
          }),
          catchError(error => {
            this.snackBar.open('Failed to load action', 'Close', {
              panelClass: ['custom-snackbar', 'snackbar-error']
            });
            return of(null);
          })
        ).subscribe();
    }
  }

  onSubmit(): void {
    if (this.actionForm.valid) {
      const actionData = this.actionForm.value;
      const request$ = this.actionId
        ? this.actionService.update({ ...actionData, id: this.actionId })
        : this.actionService.create(actionData);

      request$.pipe(
        tap(() => {
          this.snackBar.open('Action saved successfully!', 'Close', {
            panelClass: ['custom-snackbar', 'snackbar-success']
          });
          this.router.navigate(['/actions']);
        }),
        catchError(error => {
          this.snackBar.open('Failed to save action', 'Close', {
            panelClass: ['custom-snackbar', 'snackbar-error']
          });
          return of(null);
        })
      ).subscribe();
    }
  }
}
