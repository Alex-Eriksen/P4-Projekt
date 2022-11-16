import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ChatService } from 'src/app/services/chat.service';
import { PlayerService } from 'src/app/services/player.service';
import { FriendshipRequest } from 'src/app/_models/Friendship';
import { StaticPlayerResponse } from 'src/app/_models/Player';

@Component({
  selector: 'app-add-friend',
  templateUrl: './add-friend.component.html',
  styleUrls: ['./add-friend.component.css']
})
export class AddFriendComponent implements OnInit {

	constructor(private playerService: PlayerService, public dialogRef: MatDialogRef<AddFriendComponent>, @Inject(MAT_DIALOG_DATA) public data: number, private chatService: ChatService) { }

	error: string = "";

	playerFriends: StaticPlayerResponse[] = [];
  	recentPlayers: StaticPlayerResponse[] = [];
	allPlayers: StaticPlayerResponse[] = [];
  	recentPlayerId = [1, 2 ,3, 4];

	inputText: string = "";

  	ngOnInit(): void {
		document.body.children[6].classList.add('add-friend-overlay');
		this.playerService.getAll().subscribe({
			next: (players) => {
				this.allPlayers = players;
				this.recentPlayers = players.filter(player => this.recentPlayerId.includes(player.playerID));
			},
			error: (err) => {
				console.error(Object.values(err.error.errors).join(', '));
			},
			complete: () => {
				console.log("Fetched players");
			}
		});

		this.chatService.getAllById(this.data).subscribe((friends) => {
			if(friends != null) {
				this.playerFriends = friends.map(x => x.friendPlayer);
			}
			else
				console.log("Player has no friends");
		});
  	}

	addFriend(recentPlayer?: StaticPlayerResponse): void {
		if(this.inputText == "" && recentPlayer == null) { // Checks if player has typed a username or pressed on a recent player object
			this.error = "Please enter a username or click on a recent player"
			return;
		}


		let player = this.allPlayers.find(x => (x.playerName.toLowerCase() == this.inputText.toLowerCase()) || (x.playerID == recentPlayer?.playerID)); // Checks if player exists

		if(player != null) { // if user exists
			if(this.playerFriends != null) { // if player has friends
				if(this.playerFriends.filter(x => x.playerID === player?.playerID).length > 0) {
					this.error = "Friend is already added!";
					return;
				}
			}
			let request: FriendshipRequest = { mainPlayerID: this.data, friendPlayerID: player.playerID }
			this.chatService.create(request).subscribe({
				next: (res) => {
					console.log("Next value fetched" + res);
				},
				error: (err) => {
					console.error(Object.values(err.error.errors).join(', '));
				},
				complete: () => {
					console.log("Friendship is created");
					return;
				}
			});
		}
		else
			console.log("Player is null");
	}

	onClose(): void {
		this.dialogRef.close();
	}
}
