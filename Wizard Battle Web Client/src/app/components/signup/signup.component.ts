import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {

  error: string = '';
  form: FormGroup = this.fb.group({
    username: ['', Validators.required],
    password: ['', Validators.required],
    confirm: ['', Validators.required]
  })

  constructor(private fb: FormBuilder, private router: Router) { }

  ngOnInit(): void {
  }

  private validateForm(): Promise<boolean>
	{
		return new Promise<boolean>((resolve) =>
		{
			if (this.form.controls['password'].value === this.form.controls['confirm'].value)
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
