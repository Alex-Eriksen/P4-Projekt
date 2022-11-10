import { Component, OnInit } from '@angular/core';
import { JwtDecodePlus } from 'src/app/helpers/JWTDecodePlus';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { PlayerService } from 'src/app/services/player.service';
import { DevblogItem } from 'src/app/_models/Misc/devblog-item';
import { FeaturedItem } from 'src/app/_models/Misc/featured-item';
import { DirectPlayerResponse } from 'src/app/_models/Player';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private authenticationService: AuthenticationService, private playerService: PlayerService) { }

  featuredItems: FeaturedItem[] = [];

  devblogItems: DevblogItem[] = [];

  newItem: FeaturedItem = { itemName: 'Wise Wizard Skin', itemImageLocation: "../../../../assets/featured-item-card.jpg", itemPrice: 375 };

  newDevblog: DevblogItem = { title: "Wizard Battle Version 1.0 Release date", description: "We are not finished. Mf. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Omnis, consectetur repellat.", imageLocation: "../../../assets/devblog-image.jpg"}

  player: DirectPlayerResponse = { playerID: 0, account: {accountID: 0, email: "" }, playerName: "", icon: {iconID: 0, iconName: ""}, playerStatus: "", experiencePoints: 0, maxHealth: 0, maxMana: 0, knowledgePoints: 0, timeCapsules: 0, matchWins: 0, matchLosses: 0, timePlayedMin: 0 };

  ngOnInit(): void {
    this.authenticationService.OnTokenChanged.subscribe(x => {
      this.player.playerID = JwtDecodePlus.jwtDecode(x).nameid; // Gets playerId
      this.playerService.getById(this.player.playerID).subscribe(data => this.player = data);
    });

    for(let i = 0; i < 4; i++) {
      this.featuredItems.push(this.newItem);
      this.devblogItems.push(this.newDevblog);
    }
  }

  toHoursAndMinutes(totalMinutes: number): string {
    let hours = Math.floor(totalMinutes / 60);
    let minutes = totalMinutes % 60;

    return `${hours}h${minutes}m`;
  }
}
