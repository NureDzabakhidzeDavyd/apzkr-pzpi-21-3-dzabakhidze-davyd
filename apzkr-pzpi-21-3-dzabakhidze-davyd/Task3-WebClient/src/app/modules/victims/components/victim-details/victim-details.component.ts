import {ActivatedRoute} from "@angular/router";
import {Component, OnInit} from "@angular/core";
import {Victim} from "../../../../models";
import {VictimService} from "../../../../@core/services/victim.service";
import {MatSnackBar} from "@angular/material/snack-bar";
import {DomSanitizer} from "@angular/platform-browser";

@Component({
  selector: 'victim-details',
  templateUrl: './victim-details.component.html',
  styleUrls: ['./victim-details.component.scss']
})
export class VictimDetailsComponent implements OnInit {
  victim: Victim | undefined;
  isLoading = true;
  qrCodeUrl: any;

  constructor(
    private route: ActivatedRoute,
    private victimService: VictimService,
    private snackBar: MatSnackBar,
    private sanitizer: DomSanitizer
  ) {}

  ngOnInit(): void {
    this.loadVictimDetails();
  }

  loadVictimDetails(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.victimService.getById(id).subscribe(
        (victim: Victim) => {
          this.victim = victim;
          this.isLoading = false;
        },
        (error) => {
          this.isLoading = false;
          this.snackBar.open('Failed to load victim details', 'Close', {
            duration: 3000,
            panelClass: ['custom-snackbar', 'snackbar-error']
          });
        }
      );
    } else {
      this.isLoading = false;
    }
  }

  generateQRCode(victimId: string): void {
    this.victimService.getQRCodeById(victimId).subscribe(response => {
      const objectURL = URL.createObjectURL(response);
      this.qrCodeUrl = this.sanitizer.bypassSecurityTrustUrl(objectURL);
    });
  }

  goBack(): void {
    window.history.back();
  }
}
