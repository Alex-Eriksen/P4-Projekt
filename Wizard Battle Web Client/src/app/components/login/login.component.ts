import { Component, ErrorHandler, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { AuthenticationRequest } from 'src/app/_models/Authentication/AuthenticationRequest';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

	public request: AuthenticationRequest = { email: '', password: '' };
	private returnUrl: string = "";
	private guardType: number = 0;

  constructor(private router: Router, private authenticationService: AuthenticationService, private route: ActivatedRoute) { }

	ngOnInit(): void
	{
		this.returnUrl = this.route.snapshot.queryParams[ "returnUrl" ] || "/";
		this.guardType = this.route.snapshot.queryParams[ "guard" ] || 0;
		this.authenticationService.OnTokenChanged.subscribe((token) =>
		{
			if (token !== "")
			{
				this.router.navigate([ this.returnUrl ]);
			}
		});
	}

	public login(): void
	{
    this.authenticationService.authenticate(this.request).subscribe({
      next: () =>
      {
        this.router.navigate([ this.returnUrl ]);
      },
      error: (err) =>
      {
        console.error(Object.values(err.error.errors).join(', '));
      }
    });
	}
}
