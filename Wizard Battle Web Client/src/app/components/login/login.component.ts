import { Component, ErrorHandler, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  error: string = '';
  form: FormGroup = this.fb.group({
    username: ['', Validators.required],
    password: ['', Validators.required]
  })

  constructor(private fb: FormBuilder) { }

  ngOnInit(): void {
  }


  private validateForm(): Promise<boolean>
	{
		return new Promise<boolean>((resolve) =>
		{
			if (this.form.controls['password'].value === this.form.controls['password'].value)
			{
				resolve(true);
			}

			resolve(false);
		});
	}

  public submit(): void
	{
		this.validateForm().then((result) =>
		{
			if (result)
			{
				// this.customerService.create(this.request).subscribe({
				// 	next: () =>
				// 	{
				// 		this.router.navigate([ this.returnUrl ]);
				// 	},
				// 	error: (err) =>
				// 	{
				// 		console.error(Object.values(err.error.errors).join(', '));
				// 	}
				// });
			}
		});
	}
}
