import { Component, EventEmitter, Input, OnInit, Output, SimpleChanges } from '@angular/core';
import { ChatService } from 'src/app/services/chat.service';
import { DirectFriendshipResponse, StaticFriendshipResponse } from 'src/app/_models/Friendship';
import { MessageRequest, StaticMessageResponse } from 'src/app/_models/Message';
import { StaticPlayerResponse } from 'src/app/_models/Player';

@Component({
  selector: 'chat-box',
  templateUrl: './chat-box.component.html',
  styleUrls: ['./chat-box.component.css']
})
export class ChatBoxComponent implements OnInit {

  constructor(private chatService: ChatService) { }

  @Input() friendship: DirectFriendshipResponse;

  @Output() CloseChatWindow = new EventEmitter<any>();

  public messages: StaticMessageResponse[] = [];

  public messageRequest: MessageRequest = {senderID: 0, receiverID: 0, text: ""}

  ngOnInit(): void {
    this.chatService.GetAllMessages(this.friendship.mainPlayer.playerID, this.friendship.friendPlayer.playerID).subscribe(data => this.messages = data);
    this.messageRequest = {senderID: this.friendship.mainPlayer.playerID, receiverID: this.friendship.friendPlayer.playerID, text: ""}
  }

  closeWindow(): void {
    this.CloseChatWindow.emit();
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
