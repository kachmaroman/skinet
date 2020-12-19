import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusyService {

  busyRequestsCount = 0;

  constructor(private spinnerService: NgxSpinnerService) { }

  busy(): void {
    this.busyRequestsCount++;

    this.spinnerService.show(undefined, {
      type: 'ball-fall',
      bdColor: 'rgba(255,255,255,0.7)',
      color: '#333333',
      size: 'medium'
    })
  }

  idle(): void {
    this.busyRequestsCount--;

    if (this.busyRequestsCount <= 0) {
      this.busyRequestsCount = 0;
      this.spinnerService.hide();
    }
  }
}
