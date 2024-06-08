import {Component} from "@angular/core";
import {MatDialogRef} from "@angular/material/dialog";

@Component({
  selector: 'confirm-delete-modal',
  templateUrl: './confirm-delete-modal.component.html',
  styleUrls: ['./confirm-delete-modal.component.scss']
})
export class ConfirmDeleteModalComponent {
  constructor(public dialogRef: MatDialogRef<ConfirmDeleteModalComponent>) {}

  confirmDelete(): void {
    this.dialogRef.close(true); // Підтвердження видалення
  }

  cancelDelete(): void {
    this.dialogRef.close(false); // Скасування видалення
  }
}
