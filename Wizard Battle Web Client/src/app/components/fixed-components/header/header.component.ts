import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor() { }

  baseExp: number = 25;
  playerExp: number = 0;
  playerLvl: number = 1;
  showExp: boolean = false;
  playerName: string = "AlexTheG"
  playerKP: number = 10;
  playerCaps: number = 10;

  ngOnInit(): void {
    this.playerExp = 160;
    this.getLevel();
  }

  getLevel(): void {
    while(this.playerExp > this.baseExp) {
      if(this.playerExp >= this.baseExp) {
        this.playerExp = Math.round(this.playerExp - this.baseExp);
        this.playerLvl++;
        this.baseExp = Math.round(this.baseExp * 1.2);
      }
    }
  }
}
