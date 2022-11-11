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
  public player: DirectPlayerResponse = { playerID:0, account: { email:"", accountID:0 }, playerName:"", experiencePoints:0, maxHealth:0, maxMana:0, knowledgePoints:0, timeCapsules:0, timePlayedMin:0, icon: { iconID: 0, iconName: "" }, matchLosses:0, matchWins:0, playerStatus:"" }

  constructor(private authService:AuthenticationService, private playerService: PlayerService) { }

  ngOnInit(): void {
	  let playerId = JwtDecodePlus.jwtDecode(this.authService.AccessToken).nameid;
      this.playerService.getById(playerId).subscribe(response => { this.player = response; });
    }
  }
