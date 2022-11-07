import { animate, keyframes, state, style, transition, trigger } from '@angular/animations';
import { Component, EventEmitter, Input, OnInit, OnChanges, Output, SimpleChanges } from '@angular/core';
import { ChatService } from 'src/app/services/chat.service';
import { Message } from 'src/app/_models/Friend/Message';
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
        width: '500px',
        height: '500px',
       'background-color': 'rgba(8, 21, 37, .5)'
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
export class ChatBoxComponent implements OnInit {

  constructor(private chatService: ChatService) { }

  @Input() playerId: number;

  @Input() friend: StaticPlayerResponse;

  @Input() chatWindow: boolean; // Chat window status : Open/Closed

  @Output() closedChatWindow = new EventEmitter<any>(); // EventEmitter for chat window

  public messages: StaticMessageResponse[] = [];

  public messageRequest: MessageRequest = { senderID: 0, receiverID: 0, text: "" }

  ngOnInit(): void {
  }

  ngOnChanges() { // Gets messages if friend object is changed
    if(this.playerId != 0) {
      this.chatService.GetAllMessages(this.playerId, this.friend.playerID).subscribe(data => this.messages = data);
    }
    this.messageRequest = { senderID: this.playerId, receiverID: this.friend.playerID, text: ""} // Assigns sender & receiver for messageRequest
  }

  sendMessage(): void {
    if(this.messageRequest.text == '')
      return;

    this.chatService.SendMessage(this.messageRequest).subscribe({
      next: (response) => {
        if(this.messages == null) {
          this.messages = [];
        }
        this.messages.push(response);
      },
      complete: () => {
        this.messageRequest.text = '';
      }
    });
  }
}
