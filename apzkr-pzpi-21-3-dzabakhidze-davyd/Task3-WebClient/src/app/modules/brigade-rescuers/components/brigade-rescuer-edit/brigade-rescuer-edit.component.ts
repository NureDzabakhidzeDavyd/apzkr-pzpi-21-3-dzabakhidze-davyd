import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ActivatedRoute} from '@angular/router';
import {Brigade} from "../../../../models";
import {BrigadeRescuerService} from "../../../../@core/services/brigade-rescuer.service";
import {BrigadeService} from "../../../../@core/services/brigade.service";
import {catchError, of, tap} from "rxjs";
import {MatSnackBar} from "@angular/material/snack-bar";

@Component({
  selector: 'brigade-rescuer-edit',
  templateUrl: './brigade-rescuer-edit.component.html',
  styleUrls: ['./brigade-rescuer-edit.component.scss']
})
export class BrigadeRescuerEditComponent implements OnInit {
  brigadeRescuerForm: FormGroup;
  availableBrigades: Brigade[] = [];
  page: number = 0;
  pageSize: number = 10;
  brigadeRescuerId: string | null = null;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private brigadeRescuerService: BrigadeRescuerService,
    private brigadeService: BrigadeService,
    private snackBar: MatSnackBar
  ) {
    this.brigadeRescuerForm = this.fb.group({
      contact: this.fb.group({
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        middleName: ['', Validators.required],
        email: ['', [Validators.required, Validators.email]],
        address: ['', Validators.required],
        phone: ['', Validators.required],
        dateOfBirth: ['', Validators.required]
      }),
      position: ['', Validators.required],
      specialization: ['', Validators.required],
      brigadeId: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.brigadeRescuerId = params.get('id');
      if (this.brigadeRescuerId) {
        this.loadBrigadeRescuer(this.brigadeRescuerId);
      }
    });
    this.loadMoreRescuers();
  }

  loadMoreRescuers(): void {
    this.brigadeService.getAll(this.page, this.pageSize)
      .pipe(
        tap(rescuers => {
          this.availableBrigades = [...this.availableBrigades, ...rescuers];
          this.page++;
        })
      ).subscribe();
  }

  loadBrigadeRescuer(id: string): void {
    this.brigadeRescuerService.getById(id).subscribe(rescuer => {
      this.brigadeRescuerForm.patchValue(rescuer);
    });
  }

  onSubmit(): void {
    if (this.brigadeRescuerForm.valid) {
      const formValue = this.brigadeRescuerForm.value;
      if (this.brigadeRescuerId) {
        this.brigadeRescuerService.update({ ...formValue, id: this.brigadeRescuerId }).pipe(
          tap(() => this.snackBar.open('Brigade Rescuer updated successfully!', 'Close', {
            panelClass: ['custom-snackbar', 'snackbar-success']
          })),
          catchError(error => {
            this.snackBar.open('Failed to update Brigade Rescuer', 'Close', {
              panelClass: ['custom-snackbar', 'snackbar-error']
            });
            return of([]);
          })
        ).subscribe();
      } else {
        this.brigadeRescuerService.create(formValue).pipe(
          tap(() => this.snackBar.open('Brigade Rescuer created successfully!', 'Close', {
            panelClass: ['custom-snackbar', 'snackbar-success']
          })),
          catchError(error => {
            this.snackBar.open('Failed to create Brigade Rescuer', 'Close', {
              panelClass: ['custom-snackbar', 'snackbar-error']
            });
            return of([]);
          })
        ).subscribe();
      }
    }
  }
}
