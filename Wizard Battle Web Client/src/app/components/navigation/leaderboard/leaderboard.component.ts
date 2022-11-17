import { Component, OnInit } from '@angular/core';
import { PlayerService } from 'src/app/services/player.service';
import { StaticPlayerResponse } from 'src/app/_models/Player';

@Component({
  selector: 'app-leaderboard',
  templateUrl: './leaderboard.component.html',
  styleUrls: ['./leaderboard.component.css']
})
export class LeaderboardComponent implements OnInit {

	constructor(private playerService: PlayerService) { }

	players: StaticPlayerResponse[] = [];

	ngOnInit(): void {
		this.playerService.getAll().subscribe({
			next: (response) => {
				this.players = response.sort((a, b) => {
					return parseFloat(this.getWinrate(b)) - parseFloat(this.getWinrate(a));
				});
			},
			error: (err) => {
				console.error(Object.values(err.error.errors).join(', '));
			},
			complete: () => {
				console.log("Fetched all players");
			}
		})
	}

	public getIndex(player: StaticPlayerResponse) {
		return this.players.findIndex(x => x.experiencePoints === player.experiencePoints) + 1;
	}

	public getWinrate(player: StaticPlayerResponse) {
		let matches = player.matchWins + player.matchLosses;
		return ((player.matchWins * 100) / matches).toFixed(2);
	}

	public getLevel(player: StaticPlayerResponse) {
		return this.playerService.getLevel(player.experiencePoints);
	}

	getRandomNumber(min: number, max: number): number {
		return Math.floor(Math.random() * (Math.floor(max) - Math.ceil(min)) + Math.ceil(min));
	}
 }
