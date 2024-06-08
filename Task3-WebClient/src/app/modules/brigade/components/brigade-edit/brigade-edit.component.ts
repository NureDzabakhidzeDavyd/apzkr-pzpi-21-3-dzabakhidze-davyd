import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from '@angular/router';
import { BrigadeService } from "../../../../@core/services/brigade.service";
import { BrigadeRescuerService } from "../../../../@core/services/brigade-rescuer.service";
import { BrigadeRescuer, Brigade } from "../../../../models";
import { MatSnackBar } from "@angular/material/snack-bar";
import { tap } from "rxjs/operators";

@Component({
  selector: 'brigade-edit',
  templateUrl: './brigade-edit.component.html',
  styleUrls: ['./brigade-edit.component.scss']
})
export class BrigadeEditComponent implements OnInit {
  brigadeForm: FormGroup;
  availableRescuers: BrigadeRescuer[] = [];
  brigadeId: string | null = null;
  page: number = 0;
  pageSize: number = 10;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private brigadeService: BrigadeService,
    private brigadeRescuerService: BrigadeRescuerService,
    private snackBar: MatSnackBar
  ) {
    this.brigadeForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      brigadeRescuers: [[]]
    });
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.brigadeId = params.get('id');
      if (this.brigadeId) {
        this.loadBrigadeData(this.brigadeId);
      }
    });
    this.loadMoreRescuers();
  }

  loadBrigadeData(id: string): void {
    this.brigadeService.getById(id).subscribe((brigade: Brigade) => {
      this.brigadeForm.patchValue({
        name: brigade.name,
        description: brigade.description,
        brigadeRescuers: brigade.brigadeRescuers.map(rescuer => rescuer.id)
      });
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
    if (this.brigadeForm.valid) {
      const brigadeData = this.brigadeForm.value;
      if (this.brigadeId) {
        this.brigadeService.update({ id: this.brigadeId, ...brigadeData }).pipe(
          tap(() => this.snackBar.open('Brigade updated successfully', 'Close', { panelClass: ['custom-snackbar', 'snackbar-success'] })),
          tap(() => this.router.navigate(['/brigades']))
        ).subscribe();
      } else {
        this.brigadeService.create(brigadeData).pipe(
          tap(() => this.snackBar.open('Brigade created successfully', 'Close', { panelClass: ['custom-snackbar', 'snackbar-success'] })),
          tap(() => this.router.navigate(['/brigades']))
        ).subscribe();
      }
    }
  }
}
