import {Component, OnInit} from "@angular/core";
import {Observable} from "rxjs";
import {Contact} from "../../../../models";
import {AuthService} from "../../../../@core/services/auth.service";
import {BackupService} from "../../../../@core/services/backup.service";
import {Router} from "@angular/router";
import { FileSaverService } from 'ngx-filesaver';

@Component({
  selector: 'app-profile-details',
  templateUrl: './profile-details.component.html',
  styleUrls: ['./profile-details.component.scss']
})
export class ProfileDetailsComponent implements OnInit{
  user$: Observable<Contact> | undefined;

  constructor(private authService: AuthService,
              private backupService: BackupService,
              private router: Router,
              private fileServerService: FileSaverService) {}

  ngOnInit(): void {
    this.user$ = this.authService.getProfile();
  }

  onLogout() {
    this.authService.logout().subscribe(() => {
      this.router.navigate(['/home']);
    });
  }

  onGetBackup() {
    this.backupService.getBackup().subscribe(response => {
      const contentDisposition = response.headers.get('Content-Disposition');
      const filenameRegex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/i;
      const matches = filenameRegex.exec(contentDisposition);

      let filename = 'backup.sql';
      if (matches != null && matches[1]) {
        filename = matches[1].replace(/['"]/g, ''); // Remove quotes
      }

      const blob = new Blob([response.body], { type: 'application/octet-stream' });
      const url = window.URL.createObjectURL(blob);
      const a = document.createElement('a');
      a.href = url;
      a.download = filename;
      a.click();
      window.URL.revokeObjectURL(url);
    });
  }
}
