import { Component, EventEmitter, OnInit, Output , AfterViewInit} from '@angular/core';
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

@Component({
  selector: 'chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit, AfterViewInit {

  @Output() openChat: EventEmitter<any> = new EventEmitter();

  playerId: number = 0;

  public connection: HubConnection;

  friends: StaticPlayerResponse[] = [];
  backupFriends: StaticPlayerResponse[] = []; // backup array of this.friends used in search function

  player: DirectPlayerResponse = { playerID: 0, account: {accountID: 0, email: "" }, playerName: "", icon: {iconID: 0, iconName: ""}, playerStatus: "", experiencePoints: 0, maxHealth: 0, maxMana: 0, knowledgePoints: 0, timeCapsules: 0, matchWins: 0, matchLosses: 0, timePlayedMin: 0};
  friend: StaticPlayerResponse = { playerID: 0,  accountID: 0, playerName: "", icon: {iconID: 0, iconName: ""}, playerStatus: "", experiencePoints: 0, matchWins: 0, matchLosses: 0, timePlayedMin: 0};

  isChatWindowOpen: boolean = false;
  friendListOpen: boolean = true;

  currentStatus: string = "";

  constructor(private authenticationService: AuthenticationService, private playerService: PlayerService, private chatService: ChatService, private signalrService: SignalrService) { }

  ngOnInit(): void {
    this.playerId = JwtDecodePlus.jwtDecode(this.authenticationService.AccessToken).nameid; // Gets playerId
    this.playerService.OnStatusChanged.subscribe((status: string) => {
      if(status === undefined)
        return
      this.getClass(status);

    });

    this.playerService.getById(this.playerId).subscribe(data => this.player = data);

    this.chatService.getAll(this.playerId).subscribe(data => { // Fetch friends
      this.friends = data;
      this.backupFriends = data;
    });

    this.signalrService.OnStatusChanged.subscribe(() => { // If a friend changes status fetch friends again
      this.chatService.getAll(this.playerId).subscribe(data => {
        this.friends = data;
        this.backupFriends = data;
        if(this.friend.playerID != 0) {
          this.openMessages(this.friends.find(x => x.playerID = this.friend.playerID)!);
        }
      })
    })
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
    if(!this.isChatWindowOpen)
      this.toggleChatWindow();

    this.friend = newFriend;
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
		  if(newFilter.indexOf(searchText.toLowerCase()) !== -1){
			  output.push(friend);
		  }
	  }
	  this.friends = output;
  }

  getClass(status: string): void {
    switch(status) {
      case "Online": this.currentStatus="text-success"; break;
      case "Offline": this.currentStatus="text-danger"; break;
      default: this.currentStatus="text-warning"; break;
    }
  }
}
