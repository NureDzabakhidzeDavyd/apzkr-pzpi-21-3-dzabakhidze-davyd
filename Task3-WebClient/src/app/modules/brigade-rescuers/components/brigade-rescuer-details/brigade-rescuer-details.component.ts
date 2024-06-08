import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BrigadeRescuerService } from '../../../../@core/services/brigade-rescuer.service';
import { BrigadeRescuer } from '../../../../models';
import {DomSanitizer} from "@angular/platform-browser";

@Component({
  selector: 'app-brigade-rescuer-details',
  templateUrl: './brigade-rescuer-details.component.html',
  styleUrls: ['./brigade-rescuer-details.component.scss']
})
export class BrigadeRescuerDetailsComponent implements OnInit {
  brigadeRescuer: BrigadeRescuer | null = null;
  qrCodeUrl: any;

  constructor(
    private route: ActivatedRoute,
    private brigadeRescuerService: BrigadeRescuerService,
    private sanitizer: DomSanitizer
  ) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.brigadeRescuerService.getById(id).subscribe(rescuer => {
        this.brigadeRescuer = rescuer;
      });
    }
  }

  generateQRCode(id: string) {
    this.brigadeRescuerService.getQRCodeById(id).subscribe(response => {
      const objectURL = URL.createObjectURL(response);
      this.qrCodeUrl = this.sanitizer.bypassSecurityTrustUrl(objectURL);
    });
  }
}
