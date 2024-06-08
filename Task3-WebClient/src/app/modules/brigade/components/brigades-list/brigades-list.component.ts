import {Component, OnInit} from "@angular/core";
import {Brigade} from "../../../../models";
import {BrigadeService} from "../../../../@core/services/brigade.service";
import {Router} from "@angular/router";
import {MatDialog} from "@angular/material/dialog";
import {
  ConfirmDeleteModalComponent
} from "../../../../@shared/components/confirm-delete-modal/confirm-delete-modal.component";

@Component({
  selector: 'app-brigade-list',
  templateUrl: './brigades-list.component.html',
  styleUrls: ['./brigades-list.component.scss']
})
export class BrigadeListComponent implements OnInit {
  public displayedColumns: string[] = ['name', 'description', 'brigadeSize'];
  public brigades: Brigade[] = [];
  public totalRecords: number = this.brigades.length;
  public pageSize = 10;
  public currentPage = 1;

  constructor(private brigadeService: BrigadeService, private router: Router, private dialog: MatDialog) {}

  ngOnInit(): void {
    this.loadBrigades();
  }

  loadBrigades(): void {
    this.brigadeService.getAll(this.currentPage, this.pageSize).subscribe(
      (brigades: Brigade[]) => {
        this.brigades = brigades;
        this.totalRecords = brigades.length;
      }
    );
  }

  deleteBrigade(brigadeId: string): void {
    this.brigadeService.delete(brigadeId).subscribe(
      () => {
        this.loadBrigades();
      },
      (error) => {
        console.error('Помилка видалення бригади:', error);
      }
    );
  }

  public deleteBrigadeConfirmation(brigadeId: string): void {
    const dialogRef = this.dialog.open(ConfirmDeleteModalComponent, {
      width: '250px',
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.deleteBrigade(brigadeId);
      }
    });
  }

  openBrigadeDetails(brigadeId: string): void {
    this.router.navigate(['brigades', brigadeId]);
  }

  onPageChange(page: number): void {
    this.currentPage = page;
    this.loadBrigades();
  }

  editBrigadeConfirmation(brigadeId: string) {
    this.router.navigate(['brigades', brigadeId, 'edit']);
  }

  createNewBrigade(): void {
    this.router.navigate(['brigades/create']);
  }
}
