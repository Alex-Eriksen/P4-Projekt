import { Component, OnInit } from '@angular/core';

import { PlayerService } from 'src/app/services/player.service';

import { AccountService } from 'src/app/services/account.service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { AuthenticationRequest } from 'src/app/_models/Authentication';

import { DirectPlayerResponse } from 'src/app/_models/Player';
@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})

export class ProfileComponent implements OnInit {
  public request: AuthenticationRequest = { email: '', password: '' };
 Players:DirectPlayerResponse[] = [];

  constructor() { }
  ngOnInit(): void {
    this.Players.push({
      playerID:1,
      account:{
        email: "test@test.com",
        accountID:1
      },
      playerName: "NickTheG",
      icon: {iconID: 0, iconName: ""},
      experiencePoints: 20,
      maxHealth: 100,
      playerStatus: "",
      maxMana: 80,
      knowledgePoints: 0,
      timeCapsules: 120,
      matchWins: 0,
      matchLosses: 0,
      timePlayedMin: 0
    });

player:DirectPlayerResponse = {
  playerID:0,
  account:{
    email:"",
    accountID:0
  },
  playerName:"",
  experiencePoints:0,
  maxHealth:0,
  maxMana:0,
  knowledgePoints:0,
  timeCapsules:0,
  TimePlayed:"",
}
  constructor(private authService:AuthenticationService,private ActService:AccountService) { }
  ngOnInit(): void {
      this.ActService.getByToken(this.authService.AccessToken).subscribe(res=>
        {this.player.account=res
          this.player.playerID=res.player.playerID
          this.player.playerName=res.player.playerName
          this.player.experiencePoints=res.player.experiencePoints
          this.player.maxHealth=res.player.maxHealth
          this.player.maxMana=res.player.maxMana
          this.player.knowledgePoints=res.player.knowledgePoints
          this.player.timeCapsules=res.player.timeCapsules
          this.player.TimePlayed=""

          this.Players.push(this.player)
        });
   
    };

  }


