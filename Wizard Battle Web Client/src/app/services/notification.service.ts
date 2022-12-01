import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr'

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  	constructor(private toastr: ToastrService) { }

  	public success(message: string): void {
	  	this.toastr.success(message, "Success");
  	}

	public info(message: string): void {
		this.toastr.info(message, "Info");
	}

	public error(message: string): void {
		this.toastr.error(message, "Error");
	}
}
