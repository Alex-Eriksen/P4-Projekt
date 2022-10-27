import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { JwtDecodePlus } from 'src/app/helpers/JWTDecodePlus';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { PlayerService } from 'src/app/services/player.service';
import { DirectPlayerResponse } from 'src/app/_models/Player';
import { PlayerChat } from './player';
import {ScrollingModule} from '@angular/cdk/scrolling';
import { ChatService } from 'src/app/services/chat.service';
import { StaticFriendshipResponse } from 'src/app/_models/Friendship';

@Component({
  selector: 'chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {

  @Output() openChat: EventEmitter<any> = new EventEmitter();

  chatOpen: boolean = true;
  chatTab: string = 'chat';
  private playerId: number = 0;
  playerName: string = "";
  friendships: StaticFriendshipResponse[] = [];
  constructor(private authenticationService: AuthenticationService, private playerService: PlayerService, private chatService: ChatService) { }

  ngOnInit(): void {
    this.authenticationService.OnTokenChanged.subscribe(x =>
      {
        this.playerId = JwtDecodePlus.jwtDecode(x).nameid; // Gets playerId
        this.playerService.getById(this.playerId).subscribe(data => this.playerName = data.playerName)
        this.chatService.GetAll(this.playerId).subscribe(data => this.friendships = data);
      })
    }

  toggleChat(newChatTab?: string) {
    if(newChatTab == null) { // Close tab
      this.chatOpen = false;
      this.chatTab = '';
      this.openChat.emit(null);
    }
    else {
      if(newChatTab != null && !this.chatOpen) { // if new Chat is not null and chat is not open
        this.openChat.emit(null);
        this.chatTab = newChatTab!;
        this.chatOpen = true;
      }
      else if(this.chatOpen && this.chatTab != newChatTab) {
        this.chatTab = newChatTab!;
      }
    }
  }
}
