import {Component, OnInit} from '@angular/core';
import {Brigade} from "../../../../models";
import {ActivatedRoute} from "@angular/router";
import {BrigadeService} from "../../../../@core/services/brigade.service";

@Component({
  selector: 'app-brigade-details',
  templateUrl: './brigade-details.component.html',
  styleUrls: ['./brigade-details.component.scss']
})
export class BrigadeDetailsComponent implements OnInit{
  brigade: Brigade | undefined;

  constructor(private route: ActivatedRoute, private brigadeService: BrigadeService) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      const brigadeId = params['id']; // Get doctor ID from route
      this.brigadeService.getById(brigadeId).subscribe((brigade: Brigade) => {
        this.brigade = brigade; // Fetch doctor data
      });
    });
  }
}
