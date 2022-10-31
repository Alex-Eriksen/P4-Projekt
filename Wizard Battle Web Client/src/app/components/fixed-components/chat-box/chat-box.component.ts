import { animate, keyframes, state, style, transition, trigger } from '@angular/animations';
import { Component, EventEmitter, Input, OnInit, OnChanges, Output, SimpleChanges } from '@angular/core';
import { ChatService } from 'src/app/services/chat.service';
import { DirectFriendshipResponse, StaticFriendshipResponse } from 'src/app/_models/Friendship';
import { MessageRequest, StaticMessageResponse } from 'src/app/_models/Message';
import { DirectPlayerResponse, StaticPlayerResponse } from 'src/app/_models/Player';

@Component({
  selector: 'chat-box',
  templateUrl: './chat-box.component.html',
  styleUrls: ['./chat-box.component.css'],
  animations: [
    trigger('openClose', [
      state('open', style({
        overflow: 'hidden',
        width: '500px',
        height: '500px',
       'background-color': 'rgba(8, 21, 37, .5)'
      })),
      state('closed', style({
        opacity: 0,
        overflow: 'hidden',
        height: '0px',
        width: '0px'
      })),
      transition('* => *', animate('250ms ease-in'))
    ])
  ]
})
export class ChatBoxComponent implements OnInit {

  constructor(private chatService: ChatService) { }

  @Input() friend: StaticPlayerResponse;

  @Input() playerId: number;

  @Input() chatWindow: boolean;

  @Output() closedChatWindow = new EventEmitter<any>();

  public messages: StaticMessageResponse[] = [];

  public messageRequest: MessageRequest = { senderID: 0, receiverID: 0, text: "" }

  ngOnInit(): void {
    this.chatService.GetAllMessages(this.playerId, this.friend.playerID).subscribe(data => this.messages = data);
    this.messageRequest = { senderID: this.playerId, receiverID: this.friend.playerID, text: ""}
  }

  ngOnChanges() { // Gets messages if friend object is changed
    this.chatService.GetAllMessages(this.playerId, this.friend.playerID).subscribe(data => this.messages = data);
  }

  sendMessage(): void {
    if(this.messageRequest.text === '')
      return;

    this.chatService.SendMessage(this.messageRequest).subscribe({
      next: (response) => {
        this.messages.push(response);
      },
      complete: () => {
        this.messageRequest.text = '';
      }
    })
  }
}
