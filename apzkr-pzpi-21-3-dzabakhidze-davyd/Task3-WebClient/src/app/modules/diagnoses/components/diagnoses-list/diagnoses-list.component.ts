import { Component, OnInit } from '@angular/core';
import { DiagnosisService } from 'src/app/@core/services/diagnosis.service';
import { Diagnosis } from 'src/app/models/diagnosis';
import { MatSnackBar } from '@angular/material/snack-bar';
import {Router} from "@angular/router";

@Component({
  selector: 'diagnoses-list',
  templateUrl: './diagnoses-list.component.html',
  styleUrls: ['./diagnoses-list.component.scss']
})
export class DiagnosesListComponent implements OnInit {
  public displayedColumns: string[] = ['name', 'note', 'detectionTime', 'victimId', 'actions'];
  public diagnoses: Diagnosis[] = [];
  public totalRecords: number = 0;
  public pageSize = 10;
  public currentPage = 1;

  constructor(
    private diagnosisService: DiagnosisService,
    private snackBar: MatSnackBar,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadDiagnoses();
  }

  loadDiagnoses(): void {
    this.diagnosisService.getAll(this.currentPage, this.pageSize).subscribe(
      (data: Diagnosis[]) => {
        this.diagnoses = data;
        this.totalRecords = data.length; // Assuming API returns the correct length
      },
      (error) => {
        this.snackBar.open('Error fetching diagnoses', 'Close', {
          duration: 3000,
          panelClass: ['error-snackbar']
        });
        this.diagnoses = [];
      }
    );
  }

  deleteDiagnosis(diagnosisId: string): void {
    this.diagnosisService.delete(diagnosisId).subscribe(
      () => {
        this.loadDiagnoses();
      },
      (error) => {
        this.snackBar.open('Error deleting diagnosis', 'Close', {
          duration: 3000,
          panelClass: ['error-snackbar']
        });
      }
    );
  }

  createNewDiagnosis(): void {
    this.router.navigate(['brigade-rescuers/create']);
  }

  onPageChange(page: number): void {
    this.currentPage = page;
    this.loadDiagnoses();
  }
}
