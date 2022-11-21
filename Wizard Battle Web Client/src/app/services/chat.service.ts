import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { DirectFriendshipResponse, FriendshipRequest, StaticFriendshipResponse } from '../_models/Friendship';
import { MessageRequest } from '../_models/Message';
import { StaticMessageResponse } from '../_models/Message/StaticMessageResponse';
import { StaticPlayerResponse } from '../_models/Player';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  private url: string = environment.ApiUrl + "/Friendship";
  private chatUrl: string = environment.ApiUrl + "/Chat";

  private ChatSubject: BehaviorSubject<boolean>;
  public OnChatChanged: Observable<boolean>;

  constructor(private http: HttpClient) {
	this.ChatSubject = new BehaviorSubject<boolean>(true);
    this.OnChatChanged = this.ChatSubject.asObservable();
  }

  public getAllById(playerId: number): Observable<StaticFriendshipResponse[]> { // Gets all friendship displayed in the friendlist
    return this.http.get<StaticFriendshipResponse[]>(`${this.url}/${playerId}`)
  }

  public getById(playerId: number, friendId: number): Observable<DirectFriendshipResponse> { // Gets all friendship displayed in the friendlist
    return this.http.get<DirectFriendshipResponse>(`${this.url}?mainPlayerId=${playerId}&friendPlayerId=${friendId}`);
  }

  public create(request: FriendshipRequest): Observable<DirectFriendshipResponse> { //
    return this.http.post<DirectFriendshipResponse>(`${this.url}`, request)
  }

  public update(request: FriendshipRequest): Observable<DirectFriendshipResponse> {
    return this.http.put<DirectFriendshipResponse>(`${this.url}`, request);
  }

  public delete(request: FriendshipRequest): Observable<DirectFriendshipResponse> {
    return this.http.delete<DirectFriendshipResponse>(`${this.url}?MainPlayerID=${request.mainPlayerID}&FriendPlayerID=${request.friendPlayerID}`);
  }

  public getAllMessages(playerId: number, friendId: number): Observable<StaticMessageResponse[]> {
    return this.http.get<StaticMessageResponse[]>(`${this.chatUrl}?mainPlayerId=${playerId}&friendPlayerId=${friendId}`);
  }

  public createMessage(request: MessageRequest): Observable<StaticMessageResponse> {
    return this.http.post<StaticMessageResponse>(this.chatUrl, request);
  }

  public deleteMessage(request: MessageRequest): Observable<StaticMessageResponse> {
    return this.http.delete<StaticMessageResponse>(this.chatUrl, {body: request});
  }

  	public toggleChat(isOpen: boolean): void {
	  	this.ChatSubject.next(isOpen);
  	}
}
