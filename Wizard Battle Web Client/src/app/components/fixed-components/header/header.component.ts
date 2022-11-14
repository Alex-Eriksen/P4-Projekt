import { Component, HostListener, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Subject } from 'rxjs';
import { JwtDecodePlus } from 'src/app/helpers/JWTDecodePlus';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { PlayerService } from 'src/app/services/player.service';
import { DirectPlayerResponse } from 'src/app/_models/Player';
import { ChangeIconComponent } from '../../modals/change-icon/change-icon.component';


@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor(private authenticationService: AuthenticationService, private playerService: PlayerService, private dialog: MatDialog) {
    this.setTimeout();
    this.userInactive.subscribe(() => {
		if(!this.isAway) {
			this.playerService.changeStatus(this.playerId, "Away").subscribe(() => this.isAway = true)
		}
	});
  }

  isAway: boolean = false;

  userActivity: any;
  userInactive: Subject<any> = new Subject();

  baseExp: number = 25;
  playerCurrentXp: number = 0;
  playerLvl: number = 1;
  playerId: number = 0;

  player: DirectPlayerResponse = { playerID: 0, account: {accountID: 0, email: "" }, playerName: "", icon: {iconID: 0, iconName: ""}, playerStatus: "", experiencePoints: 0, maxHealth: 0, maxMana: 0, knowledgePoints: 0, timeCapsules: 0, matchWins: 0, matchLosses: 0, timePlayedMin: 0 };

  ngOnInit(): void {
    this.playerId = JwtDecodePlus.jwtDecode(this.authenticationService.AccessToken).nameid;

	this.playerService.OnProfileUpdated.subscribe(() => {
		setTimeout(() => {
			this.playerService.getById(this.playerId).subscribe(data => this.player = data)
		  }, 1500);
	});

    this.playerService.changeStatus(this.playerId, "Online").subscribe({
      next: (playerResponse) => {
        this.player = playerResponse;
      },
      complete: () => {
        this.getLevel(this.player.experiencePoints); // Gets level of player after player data is assigned
      }
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
    this.playerService.changeStatus(this.playerId, "Offline").subscribe();
  }

  setTimeout() {
    this.userActivity = setTimeout(() => this.userInactive.next(undefined), 300000) // Sets timeout to be 5 minutes
  }

  @HostListener('window:mousemove') refreshUserState() {
    if(this.isAway || this.player.playerStatus == 'Offline') { // Sets player status to Online if they were away and came back
      this.playerService.changeStatus(this.playerId, "Online").subscribe(() => this.isAway = false);
    }
    clearTimeout(this.userActivity);
    this.setTimeout();
  }

  openChangeIcon(): void {
    let dialogRef = this.dialog.open(ChangeIconComponent, {
      backdropClass: 'cdk-overlay-transparent-backdrop',
      hasBackdrop: true,
      width: '900px',
      maxWidth: '100vw',
      height: '340px',
      disableClose: true,
	  exitAnimationDuration: "0s"
    });

    dialogRef.afterClosed().subscribe(() => {
      console.log("Dialog has been closed");
    });
  }
}
