import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { JwtDecodePlus } from 'src/app/helpers/JWTDecodePlus';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { PlayerService } from 'src/app/services/player.service';
import { DirectPlayerResponse } from 'src/app/_models/Player';
import { PlayerChat } from './player';
import { ScrollingModule } from '@angular/cdk/scrolling';
import { ChatService } from 'src/app/services/chat.service';
import { DirectFriendshipResponse, StaticFriendshipResponse } from 'src/app/_models/Friendship';

@Component({
  selector: 'chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {

  @Output() openChat: EventEmitter<any> = new EventEmitter();

  playerName: string = "";
  playerId: number = 0;

  backupFriends: DirectFriendshipResponse[] = [];
  friendships: DirectFriendshipResponse[] = [];
  currentFriendship: DirectFriendshipResponse;

  isChatWindowOpen: boolean = false;
  friendListOpen: boolean = true;

  constructor(private authenticationService: AuthenticationService, private playerService: PlayerService, private chatService: ChatService) { }

  ngOnInit(): void {
    this.authenticationService.OnTokenChanged.subscribe(x => {
      this.playerId = JwtDecodePlus.jwtDecode(x).nameid; // Gets playerId
      this.playerService.getById(this.playerId).subscribe(data => this.playerName = data.playerName)
      this.chatService.GetAll(this.playerId).subscribe(data => {
        this.friendships = data;
        this.backupFriends = data;
      });
    })
  }

  getFriendName(friendship: DirectFriendshipResponse) {
    if(this.playerId != friendship.mainPlayer.playerID) {
      return friendship.mainPlayer.playerName;
    }
    return friendship.friendPlayer.playerName;
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

    if(this.currentFriendship != null) {
      this.isChatWindowOpen = !this.isChatWindowOpen;
    }
  }

  openMessages(friendship: DirectFriendshipResponse) {
    if(!this.isChatWindowOpen)
      this.toggleChatWindow();

    this.currentFriendship = friendship;
  }

  searchFriends(friends: DirectFriendshipResponse[], searchText: string):any {
    if (!searchText) { // if input is null show all friends
      this.friendships = this.backupFriends;
      return;
    }
	  let output: DirectFriendshipResponse[] = [];
	  for(let friend of friends) {
      let newFilter: string = "";
      if(this.playerId == friend.mainPlayer.playerID) {
        newFilter = `${friend.friendPlayer.playerName.toLowerCase()}`;
      } else {
        newFilter = `${friend.mainPlayer.playerName.toLowerCase()}`;
      }
		  if(newFilter.indexOf(searchText.toLowerCase()) !== -1){
			  output.push(friend);
		  }
	  }
	  this.friendships = output;
  }
}
