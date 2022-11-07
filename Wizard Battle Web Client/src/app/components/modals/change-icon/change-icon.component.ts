import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { JwtDecodePlus } from 'src/app/helpers/JWTDecodePlus';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { IconService } from 'src/app/services/icon.service';
import { PlayerService } from 'src/app/services/player.service';
import { IconResponse } from 'src/app/_models/Icon';
import { DirectPlayerResponse, PlayerRequest } from 'src/app/_models/Player';

@Component({
  selector: 'app-change-icon',
  templateUrl: './change-icon.component.html',
  styleUrls: ['./change-icon.component.css']
})
export class ChangeIconComponent implements OnInit {

  icons: IconResponse[] = [];

  chosenIcon: IconResponse = { iconID: 0, iconName: "" };

  playerId: number = 0;

  player: DirectPlayerResponse = { playerID: 0, account: {accountID: 0, email: "" }, playerName: "", icon: {iconID: 0, iconName: ""}, playerStatus: "", experiencePoints: 0, maxHealth: 0, maxMana: 0, knowledgePoints: 0, timeCapsules: 0, matchWins: 0, matchLosses: 0, timePlayedMin: 0 };

  public playerRequest: PlayerRequest = { playerName: "", iconID: 0, experiencePoints: 0, knowledgePoints: 0, timeCapsules: 0, matchWins: 0, matchLosses: 0, timePlayedMin: 0 };

  constructor(public dialogRef: MatDialogRef<ChangeIconComponent>, @Inject(MAT_DIALOG_DATA) public data: any,
  private playerService: PlayerService,
  private authenticationService: AuthenticationService,
  private iconService: IconService,
  private router: Router) { }

  ngOnInit(): void {
    this.authenticationService.OnTokenChanged.subscribe(x => {
      this.playerId = JwtDecodePlus.jwtDecode(x).nameid; // Gets playerId
      this.playerService.getById(this.playerId).subscribe(x => {
        this.playerRequest.playerName = x.playerName;
        Object.assign(this.playerRequest, x);
      });
      this.iconService.getAll().subscribe(data => this.icons = data);
    })

    // Tager fat i cdk-overlay-container i body og tilføjer en klasse så dens position absolute kan manipuleres
    document.body.children[6].classList.add('change-icon-overlay'); // Tilføjer sagt klasse til sidste element i body
  }

  onClose(): void {
    this.dialogRef.close(); // Lukker dialog
  }

  saveChanges(): void {
    this.playerRequest.iconID = this.chosenIcon.iconID;
    this.playerService.update(this.playerId, this.playerRequest).subscribe(() => {
      window.location.reload();
    });
  }


  changeIcon(iconElement: HTMLElement, icon: IconResponse) {
    let grid = document.getElementById('icon-grid')?.children;
    for(let i = 0; i < grid!.length; i++) {
      grid![i].classList.remove('active-icon')
    }
    this.chosenIcon = icon;
    iconElement.classList.add('active-icon')
  }
}
