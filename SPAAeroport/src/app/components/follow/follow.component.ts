import { Component, OnInit, Inject } from '@angular/core';
import { Notification } from 'src/app/models/Notification';
import { FlightService } from 'src/app/services/flight.service';
import { MAT_DIALOG_DATA, MatSnackBar } from '@angular/material';
import { TranslateService } from '@ngx-translate/core';

import { FormControl, Validators, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-follow',
  templateUrl: './follow.component.html',
  styleUrls: ['./follow.component.css']
})
export class FollowComponent implements OnInit {

  // Notification initial value
  notification = {
    volCeduleId: null,
    numTel: '',
    dateInscription: null,
    dateArret: null
  };

  // Control phone number in the form
  form = new FormGroup({
    numTel: new FormControl('', [Validators.required, Validators.pattern("^[0-9]{10}$")]),
  });

  constructor(
    private flightService: FlightService,
    private snackBar: MatSnackBar,
    private translate: TranslateService,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) { }

  ngOnInit() { }

  addNotification(volId: string, phone: string, numVol: any) {
    // Init notification obj
    const notification = new Notification();
    notification.volCeduleId = parseInt(volId);
    notification.numTel = phone;
    notification.dateInscription = new Date();

    this.flightService.saveNotification(notification).subscribe(() => {
      this.snackBar.open(`${this.translate.instant('Dialog.SnackBar.Success')} ${numVol.textContent}`, this.translate.instant('Dialog.SnackBar.Dismiss'),
        { duration: 4000, panelClass: ['snackbar-success'] });
      // Clear input field
      this.notification.numTel = '';
    }, () => {
      this.snackBar.open(`${this.translate.instant('Dialog.SnackBar.Invalid')}`, this.translate.instant('Dialog.SnackBar.Dismiss'), { duration: 4000, panelClass: ['snackbar-danger'] });
    });
  }

}
