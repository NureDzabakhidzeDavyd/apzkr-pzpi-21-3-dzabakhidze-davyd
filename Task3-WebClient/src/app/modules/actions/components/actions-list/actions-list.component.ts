import {Component, OnInit} from '@angular/core';
import {Action} from "../../../../models";
import {Router} from "@angular/router";
import {MatDialog} from "@angular/material/dialog";
import {
  ConfirmDeleteModalComponent
} from "../../../../@shared/components/confirm-delete-modal/confirm-delete-modal.component";
import {ActionService} from "../../../../@core/services/action.service";

@Component({
  selector: 'app-actions-list',
  templateUrl: './actions-list.component.html',
  styleUrls: ['./actions-list.component.scss']
})
export class ActionsListComponent implements OnInit {
  public displayedColumns: string[] = ['name', 'description', 'actionTime', 'actionType', 'actionPlace'];
  public actions: Action[] = [];
  public totalRecords: number = this.actions.length;
  public pageSize = 10;
  public currentPage = 1;

  constructor(private actionService: ActionService, private router: Router, private dialog: MatDialog) {}

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    this.actionService.getAll(this.currentPage, this.pageSize).subscribe(
      (action: Action[]) => {
        this.actions = action;
        this.totalRecords = action.length;
      }
    );
  }

  deleteBrigade(brigadeId: string): void {
    this.actionService.delete(brigadeId).subscribe(
      () => {
        this.loadData();
      },
      (error) => {
        console.error('Помилка видалення бригади:', error);
      }
    );
  }

  public deleteConfirmation(brigadeId: string): void {
    const dialogRef = this.dialog.open(ConfirmDeleteModalComponent, {
      width: '250px',
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.deleteBrigade(brigadeId);
      }
    });
  }

  openDetails(id: string): void {
    this.router.navigate(['actions', id]);
  }

  onPageChange(page: number): void {
    this.currentPage = page;
    this.loadData();
  }

  openEdit(id: string) {
    this.router.navigate(['actions', id, 'edit']);
  }

  createNew(): void {
    this.router.navigate(['actions/create']);
  }
}
