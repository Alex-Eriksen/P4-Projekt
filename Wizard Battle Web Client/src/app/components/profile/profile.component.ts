import { Component, OnInit } from '@angular/core';
import { DirectPlayerResponse } from 'src/app/_models/Player';
@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
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
      icon: {iconID: 0, iconLocation: ""},
      experiencePoints: 20,
      maxHealth: 100,
      playerStatus: "",
      maxMana: 80,
      knowledgePoints: 0,
      timeCapsules: 120,
      TimePlayed: "2hours"


    });
  }

}
