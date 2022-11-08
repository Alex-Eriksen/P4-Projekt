import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { BehaviorSubject, Observable } from 'rxjs';
import { Message } from '../_models/Friend/Message';
import { MessageRequest } from '../_models/Message';
import { AuthenticationService } from './authentication.service';

@Injectable({
  providedIn: 'root'
})

export class SignalrService {

  private MessageSubject: BehaviorSubject<Message>;
  public OnMessageChanged: Observable<Message>;

  constructor(private authenticationService: AuthenticationService) {
    this.MessageSubject = new BehaviorSubject<Message>(new Message());
    this.OnMessageChanged = this.MessageSubject.asObservable();
  }

  private userId: string = "";
  private connectionId: string = "";

  private hubConnection: signalR.HubConnection
  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
                              .withUrl('http://localhost:5000/chatsocket', {
                              accessTokenFactory: () => this.authenticationService.AccessToken,
                              skipNegotiation: true,
                              transport: signalR.HttpTransportType.WebSockets})
                              .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .then(() => this.GetConnectionId())
      .then(() => this.GetUserId())
      .catch(err => console.log('Error while starting connection: ' + err));

    this.hubConnection.on("FriendStatus", (message: string) => {
      console.log(message);
    })

    this.hubConnection.on("ReceiveUserMessage", (message) => {
      this.MessageSubject.next(message);
    });

    this.hubConnection.on("OnConnect", (message: string) => {
      console.log(message);
    })
  }

  private GetConnectionId = () => this.hubConnection.invoke('GetConnectionId').then((data) => this.connectionId = data); // Gets

  private GetUserId = () => this.hubConnection.invoke('GetUserId').then((data) =>  this.userId = data); // Gets user id
}
