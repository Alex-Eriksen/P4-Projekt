import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { PlayerService } from 'src/app/services/player.service';
import { AccountRequest } from 'src/app/_models/Account';
import { AuthenticationRequest } from 'src/app/_models/Authentication';
import { PlayerRequest } from 'src/app/_models/Player';
import { PlayerAccountRequest } from 'src/app/_models/Player/PlayerAccountRequest';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {

  private returnUrl: string = "";
	public passwordValidator: string = '';
  public accountRequest: AccountRequest = { email: '', password: ''};
	public playerRequest: PlayerRequest = { playerName: "", iconID: 1, experiencePoints: 0, knowledgePoints: 0, timeCapsules: 0, matchWins: 0, matchLosses: 0, timePlayedMin: 0 };
	private request: PlayerAccountRequest = { account: this.accountRequest, player: this.playerRequest };
  error: string = '';

  constructor(private authenticationService: AuthenticationService, private router: Router, private route: ActivatedRoute, private playerService: PlayerService) { }

  ngOnInit(): void {
    this.returnUrl = this.route.snapshot.queryParams[ "returnUrl" ] || "/";
		this.authenticationService.OnTokenChanged.subscribe((token) =>
		{
			if (token !== "")
			{
				this.router.navigate([ this.returnUrl ]);
			}
		});
  }

	private validateForm(): Promise<boolean>
	{
		return new Promise<boolean>((resolve) =>
		{
			if (this.accountRequest.password === this.passwordValidator)
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
				this.playerService.create(this.request).subscribe({
					next: () =>
					{
            let loginRequest: AuthenticationRequest = { email: '', password: ''};
            loginRequest = Object.assign(loginRequest, this.request.account);
						this.authenticationService.authenticate(loginRequest).subscribe();
					},
					error: (err) =>
					{
						console.error(Object.values(err.error.errors).join(', '));
					}
				});
			}
		});
	}

}
