import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { JwtDecodePlus } from 'src/app/helpers/JWTDecodePlus';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { PlayerService } from 'src/app/services/player.service';
import { DirectPlayerResponse, StaticPlayerResponse } from 'src/app/_models/Player';
import { PlayerChat } from './player';
import { ScrollingModule } from '@angular/cdk/scrolling';
import { ChatService } from 'src/app/services/chat.service';
import { DirectFriendshipResponse, StaticFriendshipResponse } from 'src/app/_models/Friendship';
import { delay, find } from 'rxjs';

@Component({
  selector: 'chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {

  @Output() openChat: EventEmitter<any> = new EventEmitter();

  playerId: number = 0;

  friends: StaticPlayerResponse[] = [];
  backupFriends: StaticPlayerResponse[] = []; // backup array of this.friends used in search function

  player: DirectPlayerResponse = { playerID: 0, account: {accountID: 0, email: "" }, playerName: "", playerStatus: "", experiencePoints: 0, maxHealth: 0, maxMana: 0, knowledgePoints: 0, timeCapsules: 0, TimePlayed:"" };
  friend: StaticPlayerResponse = { playerID: 0,  accountID: 0, playerName: "", playerImage: "", playerStatus: "", experiencePoints: 0, maxHealth: 0, maxMana: 0, knowledgePoints: 0, timeCapsules: 0 };

  isChatWindowOpen: boolean = false;
  friendListOpen: boolean = true;

  constructor(private authenticationService: AuthenticationService, private playerService: PlayerService, private chatService: ChatService) { }

  ngOnInit(): void {
    this.authenticationService.OnTokenChanged.subscribe(x => {
      this.playerId = JwtDecodePlus.jwtDecode(x).nameid; // Gets playerId
      this.playerService.getById(this.playerId).subscribe(data => this.player = data);
      this.chatService.GetAll(this.playerId).subscribe(data => {
        this.friends = data;
        this.backupFriends = data;
      });
    })
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

    if(this.friend.playerID != 0 || !this.isChatWindowOpen) {
      this.isChatWindowOpen = !this.isChatWindowOpen;
      console.log(this.isChatWindowOpen);
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
}
