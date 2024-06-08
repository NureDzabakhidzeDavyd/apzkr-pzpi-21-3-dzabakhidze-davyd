import {Component, OnInit} from '@angular/core';
import {BrigadeRescuer} from "../../../../models";
import {Router} from "@angular/router";
import {MatDialog} from "@angular/material/dialog";
import {BrigadeRescuerService} from "../../../../@core/services/brigade-rescuer.service";
import {
  ConfirmDeleteModalComponent
} from "../../../../@shared/components/confirm-delete-modal/confirm-delete-modal.component";
import {MatTableDataSource} from "@angular/material/table";

@Component({
  selector: 'brigade-rescuers-list',
  templateUrl: './brigade-rescuers-list.component.html',
  styleUrls: ['./brigade-rescuers-list.component.scss']
})
export class BrigadeRescuersListComponent implements OnInit {
  public displayedColumns: string[] = ['firstName', 'lastName', 'middleName', 'phone', 'email', 'address'];

  dataSource = new MatTableDataSource<BrigadeRescuer>();
  public totalRecords: number = this.dataSource.data.length;
  public pageSize = 10;
  public currentPage = 1;

  constructor(private rescuerService: BrigadeRescuerService, private router: Router, private dialog: MatDialog) {}

  ngOnInit(): void {
    this.loadBrigadeRescuers();
  }

  loadBrigadeRescuers(): void {
    this.rescuerService.getAll(this.currentPage, this.pageSize).subscribe(
      (rescuers: BrigadeRescuer[]) => {

        this.dataSource.data = rescuers;
        this.dataSource.sortingDataAccessor = (object: BrigadeRescuer, property: string) => {
          switch (property) {
            case "firstName":
              return object.contact.firstName;
            case "lastName":
              return object.contact.lastName;
            case "middleName":
              return object.contact.middleName;
            case "phone":
              return object.contact.phone;
            case "email":
              return object.contact.email;
            case "address":
              return object.contact.address;
            default:
              return object.contact.firstName;
          }
        };
      }
    );
  }

  deleteBrigadeRescuer(id: string): void {
    this.rescuerService.delete(id).subscribe(
      () => {
        this.loadBrigadeRescuers();
      },
      (error) => {
        console.error('Помилка видалення бригади:', error);
      }
    );
  }

  public deleteConfirmation(id: string): void {
    const dialogRef = this.dialog.open(ConfirmDeleteModalComponent, {
      width: '250px',
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.deleteBrigadeRescuer(id);
      }
    });
  }

  openDetails(brigadeId: string): void {
    // Assuming 'brigade-details' is the route for displaying brigade details
    this.router.navigate(['brigade-rescuers', brigadeId]);
  }

  onPageChange(page: number): void {
    this.currentPage = page;
    this.loadBrigadeRescuers();
  }

  openEdit(brigadeId: string) {
    this.router.navigate(['brigade-rescuers', brigadeId, 'edit']);
  }

  createNewBrigade(): void {
    this.router.navigate(['brigade-rescuers/create']);
  }
}
