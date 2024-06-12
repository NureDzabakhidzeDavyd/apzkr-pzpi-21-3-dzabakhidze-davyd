import { Component } from '@angular/core';
import {MatTableDataSource} from "@angular/material/table";
import {VictimService} from "../../../../@core/services/victim.service";
import {Router} from "@angular/router";
import {MatDialog} from "@angular/material/dialog";
import {Contact, Victim} from "../../../../models";
import {UserService} from "../../../../@core/services/user.service";
import {
  ConfirmDeleteModalComponent
} from "../../../../@shared/components/confirm-delete-modal/confirm-delete-modal.component";

@Component({
  selector: 'app-users-list',
  templateUrl: './users-list.component.html',
  styleUrls: ['./users-list.component.scss']
})
export class UsersListComponent {
  public displayedColumns: string[] = ['firstName', 'lastName', 'middleName', 'phone', 'email', 'address'];

  dataSource = new MatTableDataSource();
  public totalRecords: number = this.dataSource.data.length;
  public pageSize = 10;
  public currentPage = 1;

  constructor(private user: UserService, private router: Router, private dialog: MatDialog) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    this.user.getAll(this.currentPage, this.pageSize).subscribe(
      (contacts: Contact[]) => {
        this.dataSource.data = contacts;
        this.dataSource.sortingDataAccessor = (object: any, property) => {
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
              return object[property];
          }
        };
      }
    );
  }

  deleteUser(userId: string): void {
    this.user.delete(userId).subscribe(
      () => {
        this.loadUsers();
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
        // Якщо користувач підтвердив видалення, виконайте логіку видалення
        this.deleteUser(brigadeId);
      }
    });
  }

  openDetails(brigadeId: string): void {
    // Assuming 'brigade-details' is the route for displaying brigade details
    this.router.navigate(['victims', brigadeId]);
  }

  onPageChange(page: number): void {
    this.currentPage = page;
    this.loadUsers();
  }

  openEdit(brigadeId: string) {
    this.router.navigate(['victims', brigadeId, 'edit']);
  }

  createNewBrigade(): void {
    // Assuming 'victims/create' is the route for creating a new brigade
    this.router.navigate(['victims/create']);
  }
}
