import { Component, EventEmitter, OnInit, Output , AfterViewInit, Input} from '@angular/core';
import { JwtDecodePlus } from 'src/app/helpers/JWTDecodePlus';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { PlayerService } from 'src/app/services/player.service';
import { DirectPlayerResponse, StaticPlayerResponse } from 'src/app/_models/Player';
import { ScrollingModule } from '@angular/cdk/scrolling';
import { ChatService } from 'src/app/services/chat.service';
import { DirectFriendshipResponse, StaticFriendshipResponse } from 'src/app/_models/Friendship';
import { delay, find } from 'rxjs';
import { HubConnection } from '@microsoft/signalr';
import * as signalR from '@microsoft/signalr';
import { SignalrService } from 'src/app/services/signalr.service';
import { MatDialog } from '@angular/material/dialog';
import { AddFriendComponent } from '../../modals/add-friend/add-friend.component';
import { animate, state, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css'],
  animations: [
    trigger('openClose', [
      state('open', style({
        width: '500px',
        height: '500px',
      })),
      state('closed', style({
        opacity: 0,
        height: '0px',
        width: '0px'
      })),
      transition('* => *', animate('250ms ease-in'))
    ])
  ]
})
export class ChatComponent implements OnInit, AfterViewInit {

	@Input() isChatOpen: boolean;

  	@Output() openChat: EventEmitter<any> = new EventEmitter();

  	playerId: number = 0;

	currentOnline: number = 0;

  	public connection: HubConnection;

  	friends: StaticPlayerResponse[] = [];
	pendingFriends: StaticPlayerResponse[] = [];
  	backupFriends: StaticPlayerResponse[] = []; // backup array of this.friends used in search function

  	player: DirectPlayerResponse = { playerID: 0, account: {accountID: 0, email: "" }, playerName: "", icon: {iconID: 0, iconName: ""}, playerStatus: "", experiencePoints: 0, maxHealth: 0, maxMana: 0, knowledgePoints: 0, timeCapsules: 0, matchWins: 0, matchLosses: 0, timePlayedMin: 0};
  	friend: StaticPlayerResponse = { playerID: 0,  accountID: 0, playerName: "", icon: {iconID: 0, iconName: ""}, playerStatus: "", experiencePoints: 0, matchWins: 0, matchLosses: 0, timePlayedMin: 0, maxHealth: 0, maxMana: 0, knowledgePoints: 0, timeCapsules: 0};
	tempFriend: StaticPlayerResponse = { playerID: 0,  accountID: 0, playerName: "", icon: {iconID: 0, iconName: ""}, playerStatus: "", experiencePoints: 0, matchWins: 0, matchLosses: 0, timePlayedMin: 0, maxHealth: 0, maxMana: 0, knowledgePoints: 0, timeCapsules: 0};

	isChatWindowOpen: boolean = false;
  	friendListOpen: boolean = true;

  	currentStatus: string = "";

	remove: boolean = false;

	isPendingOpen: boolean = true;
	isFriendTabOpen: boolean = true;

  	constructor(private authenticationService: AuthenticationService, private playerService: PlayerService, private chatService: ChatService, private signalrService: SignalrService, private dialog: MatDialog) { }

  	ngOnInit(): void {
  	  	this.playerId = JwtDecodePlus.jwtDecode(this.authenticationService.AccessToken).nameid; // Gets playerId
  	  	this.playerService.OnStatusChanged.subscribe((status: string) => {
  	  	  if(status === undefined)
  	  	    return
  	  	  this.getClass(status);

  	  	});

  	  	this.playerService.getById(this.playerId).subscribe(data => this.player = data);

		this.chatService.getAllById(this.playerId).subscribe((data) => {
			this.getFriends(data);
		})


		this.signalrService.OnFriendshipChanged.subscribe((x) => { // If a friend changes status fetch friends again
			this.chatService.getAllById(this.playerId).subscribe({
				next: (data) => {
					this.getFriends(data);
				}
	  		});
		});

  	  	this.signalrService.OnStatusChanged.subscribe(() => { // If a friend changes status fetch friends again
  	  	  	this.chatService.getAllById(this.playerId).subscribe((data) => {
				this.getFriends(data);
			})
			if(this.friend.playerID != 0) {
				this.openMessages(this.friends.find(x => x.playerID = this.friend.playerID)!);
			}
  	  	})
  	}


	getFriends(friendship: StaticFriendshipResponse[]): void {
		let pending = friendship.filter(x => x.isPending == true).map(x => x.friendPlayer); // map friendship where reqest hasnt been accepted
		for(let friend of pending) {
			this.chatService.getById(this.playerId, friend.playerID).subscribe((x) => {
				if(x.friendPlayer.playerID == this.player.playerID) {
					if(this.pendingFriends.filter(e => e.playerID === x.mainPlayer.playerID).length == 0)
						this.pendingFriends.push(x.mainPlayer);
				}
			})
		}

		var statusOrder = ["Online", "Away", "Offline"];
		this.friends = friendship.filter(x => x.isPending == false).map(x  => x.friendPlayer).sort(function(a, b) {
			return statusOrder.indexOf(a.playerStatus) - statusOrder.indexOf(b.playerStatus);
		});
		this.currentOnline = this.friends.filter(x => x.playerStatus === "Online").length;
		this.backupFriends = this.friends;
	}

  	ngAfterViewInit(): void {
  	  	this.signalrService.startConnection(); // Start connection
  	}

  	toggleFriendList(): void {
  	  	if(this.isChatWindowOpen)
  	  	  	this.toggleChatWindow();
  	  	this.friendListOpen = !this.friendListOpen;
  	  	this.openChat.emit();
  	}

  	toggleChatWindow() {
  	  	if(!this.friendListOpen)
  	  	  	this.toggleFriendList();
  	  	if(this.friend.playerID != 0) {
  	  	  	this.isChatWindowOpen = !this.isChatWindowOpen;
  	  	}
  	}

  	openMessages(newFriend: StaticPlayerResponse) {
		this.friend = newFriend;
  	  	if(!this.isChatWindowOpen)
  	    	this.toggleChatWindow();

  	}

 	searchFriends(friends: StaticPlayerResponse[], searchText: string):any {
 	  	if (!searchText) { // if input is null show all friends
 	  	  	this.friends = this.backupFriends;
 	  	  	return;
 	  	}
	  	let output: StaticPlayerResponse[] = [];
	  	for(let friend of friends) {
 	  	  	let newFilter: string = "";
 	  	  	newFilter = `${friend.playerName.toLowerCase()}`;
			if(newFilter.indexOf(searchText.toLowerCase()) !== -1) {
			  	output.push(friend);
			}
	  	}
	  	this.friends = output;
 	}

	acceptFriend(friend: StaticPlayerResponse): void {
		this.chatService.update({ mainPlayerID: this.playerId, friendPlayerID: friend.playerID}).subscribe({
			next: () => {
				this.chatService.getAllById(this.playerId).subscribe((x) => this.getFriends(x));
				this.pendingFriends = this.pendingFriends.filter(x => x.playerID != friend.playerID)
			},
			error: (err) => {
				console.error(Object.values(err.error.errors).join(', '));
			},
			complete: () => {
				console.log("Friendship is accepted");
			}
		})
	}

	removeFriend(friend: StaticPlayerResponse): void {
		this.chatService.delete({ mainPlayerID: this.playerId, friendPlayerID: friend.playerID}).subscribe({
			next: () => {
				this.chatService.getAllById(this.playerId).subscribe((x) => this.getFriends(x));
				this.pendingFriends = this.pendingFriends.filter(x => x.playerID != friend.playerID)
			},
			error: (err) => {
				console.error(Object.values(err.error.errors).join(', '));
			},
			complete: () => {
				console.log("Friendship was deleted");
			}
		})
	}

 	getClass(status: string): void {
 		switch(status) {
 	    	case "Online": this.currentStatus="online-status"; break;
 	    	case "Offline": this.currentStatus="offline-status"; break;
 	    	default: this.currentStatus="away-status"; break;
 	 	}
 	}

	openAddFriend(): void {
		let dialogRef = this.dialog.open(AddFriendComponent, {
			backdropClass: 'cdk-overlay-transparent-backdrop',
			hasBackdrop: true,
			width: '900px',
			maxWidth: '100vw',
			disableClose: true,
			exitAnimationDuration: "0s",
			data: this.player.playerID,
		});

		dialogRef.afterClosed().subscribe(() => {
			console.log("Dialog has been closed");
		});
	}
}
