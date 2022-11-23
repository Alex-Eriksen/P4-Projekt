import { Component, OnInit } from '@angular/core';

import { PlayerService } from 'src/app/services/player.service';

import { AccountService } from 'src/app/services/account.service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { AuthenticationRequest } from 'src/app/_models/Authentication';

import { DirectPlayerResponse } from 'src/app/_models/Player';
import { JwtDecodePlus } from 'src/app/helpers/JWTDecodePlus';
@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})

export class ProfileComponent implements OnInit {
   	public player: DirectPlayerResponse;

  	constructor(private authService:AuthenticationService, private playerService: PlayerService) { }

  	ngOnInit(): void {
	  	let playerId = JwtDecodePlus.jwtDecode(this.authService.AccessToken).nameid;
      	this.playerService.getById(playerId).subscribe({
		next: (response) => {
			this.player = response;
		},
		error: (err) => {
			console.error(Object.values(err.error.errors).join(', '));
		}
	  });
    }
}
