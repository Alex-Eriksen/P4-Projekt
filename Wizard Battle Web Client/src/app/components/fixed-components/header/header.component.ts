import { Component, OnInit } from '@angular/core';
import { JwtDecodePlus } from 'src/app/helpers/JWTDecodePlus';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { PlayerService } from 'src/app/services/player.service';
import { DirectPlayerResponse } from 'src/app/_models/Player';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor(private authenticationService: AuthenticationService, private playerService: PlayerService) { }

  baseExp: number = 25;
  playerCurrentXp: number = 0;
  playerLvl: number = 1;
  playerId: number = 0;
  player: DirectPlayerResponse = { playerID: 0, account: {accountID: 0, email: "" }, playerName: "", experiencePoints: 0, maxHealth: 0, maxMana: 0, knowledgePoints: 0, timeCapsules: 0, TimePlayed:"" };


  ngOnInit(): void {
    this.authenticationService.OnTokenChanged.subscribe(x =>
      {
        this.playerId = JwtDecodePlus.jwtDecode(x).nameid; // Gets playerId
        this.playerService.getById(this.playerId).subscribe({ // Gets player information
          next: (data) => {
            this.player = data; // Assigns data to player
          },
          complete: () => {
            this.getLevel(this.player.experiencePoints); // Gets level of player after player data is assigned
          }
        });
      });
    }

  // Calculates level of player
  getLevel(playerExp: number): void {
    while(playerExp > this.baseExp) {
      if(playerExp >= this.baseExp) { // if player experience is higher than level threshold
        playerExp = Math.round(playerExp - this.baseExp); // Subtracts points used for level up
        this.playerLvl++; // Level up
        this.baseExp = Math.round(this.baseExp * 1.2); // Increments base experience points
      }
    }
    this.playerCurrentXp = playerExp; // Assings local variable current player experience
  }

  signOut(): void {
    this.authenticationService.revokeToken().subscribe();
  }
}
