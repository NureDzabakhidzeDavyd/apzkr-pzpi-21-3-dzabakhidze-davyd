import { Component } from '@angular/core';
import { Victim} from "../../../../models";
import {Router} from "@angular/router";
import {MatDialog} from "@angular/material/dialog";
import {VictimService} from "../../../../@core/services/victim.service";
import {MatTableDataSource} from "@angular/material/table";

@Component({
  selector: 'victims-list',
  templateUrl: './victims-list.component.html',
  styleUrls: ['./victims-list.component.scss']
})
export class VictimsListComponent {
  public displayedColumns: string[] = ['firstName', 'lastName', 'middleName', 'phone', 'email', 'address'];

  dataSource = new MatTableDataSource();
  public totalRecords: number = this.dataSource.data.length;
  public pageSize = 10;
  public currentPage = 1;

  constructor(private victimService: VictimService, private router: Router, private dialog: MatDialog) {}

  ngOnInit(): void {
    this.loadVictims();
  }

  loadVictims(): void {
    this.victimService.getAll(this.currentPage, this.pageSize).subscribe(
      (victims: Victim[]) => {
        this.dataSource.data = victims;
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

  deleteBrigade(victimId: string): void {
    this.victimService.delete(victimId).subscribe(
      () => {
        // Видалення успішне, оновіть список бригад
        this.loadVictims();
      },
      (error) => {
        // Обробка помилки видалення, можна показати повідомлення про помилку
        console.error('Помилка видалення бригади:', error);
      }
    );
  }

  public deleteConfirmation(brigadeId: string): void {
    // const dialogRef = this.dialog.open(ConfirmDeleteModalComponent, {
    //   width: '250px',
    // });
    //
    // dialogRef.afterClosed().subscribe((result) => {
    //   if (result) {
    //     // Якщо користувач підтвердив видалення, виконайте логіку видалення
    //     this.deleteBrigade(brigadeId);
    //   }
    // });
  }

  openDetails(brigadeId: string): void {
    // Assuming 'brigade-details' is the route for displaying brigade details
    this.router.navigate(['victims', brigadeId]);
  }

  onPageChange(page: number): void {
    this.currentPage = page;
    this.loadVictims();
  }

  openEdit(brigadeId: string) {
    this.router.navigate(['victims', brigadeId, 'edit']);
  }

  createNewBrigade(): void {
    // Assuming 'victims/create' is the route for creating a new brigade
    this.router.navigate(['victims/create']);
  }
}
