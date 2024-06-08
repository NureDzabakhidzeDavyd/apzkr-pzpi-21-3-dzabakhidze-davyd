import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {MatSnackBar} from "@angular/material/snack-bar";
import {VictimService} from "../../../../@core/services/victim.service";
import {BrigadeRescuerService} from "../../../../@core/services/brigade-rescuer.service";
import {BrigadeRescuer} from "../../../../models";
import {tap, catchError, of} from "rxjs";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-victim-edit',
  templateUrl: './victim-edit-component.html',
  styleUrls: ['./victim-edit-component.scss']
})
export class VictimEditComponent implements OnInit {
  victimForm: FormGroup;
  availableRescuers: BrigadeRescuer[] = [];
  page: number = 0;
  pageSize: number = 10;
  victimId: string | null = null;

  constructor(
    private fb: FormBuilder,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute,
    private victimService: VictimService,
    private brigadeRescuerService: BrigadeRescuerService
  ) {
    this.victimForm = this.fb.group({
      contact: this.fb.group({
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        middleName: ['', Validators.required],
        email: ['', [Validators.required, Validators.email]],
        phone: ['', Validators.required],
        address: ['', Validators.required],
        dateOfBirth: ['', Validators.required]
      }),
      brigadeRescuerId: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.victimId = params.get('id');
      if (this.victimId) {
        this.loadVictim(this.victimId);
      }
    });
    this.loadMoreRescuers();
  }

  loadVictim(id: string): void {
    this.victimService.getById(id).subscribe(rescuer => {
      this.victimForm.patchValue(rescuer);
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

  onSubmit(): void {
    if (this.victimForm.valid) {
      const formValue = this.victimForm.value;
      if (this.victimId) {
        this.victimService.update({...formValue, id: this.victimId}).pipe(
          tap(() => this.snackBar.open('Victim updated successfully!', 'Close', {
            panelClass: ['custom-snackbar', 'snackbar-success']
          })),
          catchError(error => {
            this.snackBar.open(error.error.errorMessage, 'Close', {
              panelClass: ['custom-snackbar', 'snackbar-error']
            });
            return of([]);
          })
        ).subscribe();
      } else {
        this.victimService.create(formValue).pipe(
          tap(() => this.snackBar.open('Victim created successfully!', 'Close', {
            panelClass: ['custom-snackbar', 'snackbar-success']
          })),
          catchError(error => {
            this.snackBar.open('Failed to create Victim', 'Close', {
              panelClass: ['custom-snackbar', 'snackbar-error']
            });
            return of([]);
          })
        ).subscribe();
      }
    }
  }
}
