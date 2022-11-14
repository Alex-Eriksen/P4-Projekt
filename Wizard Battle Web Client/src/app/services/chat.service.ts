import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { DirectFriendshipResponse, FriendshipRequest } from '../_models/Friendship';
import { MessageRequest } from '../_models/Message';
import { StaticMessageResponse } from '../_models/Message/StaticMessageResponse';
import { StaticPlayerResponse } from '../_models/Player';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  private url: string = environment.ApiUrl + "/Friendship";
  private chatUrl: string = environment.ApiUrl + "/Chat";

  constructor(private http: HttpClient) { }

  public getAll(playerId: number): Observable<StaticPlayerResponse[]> { // Gets all friendship displayed in the friendlist
    return this.http.get<StaticPlayerResponse[]>(`${this.url}/${playerId}`)
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
    return this.http.delete<DirectFriendshipResponse>(`${this.url}?MainPlayerID=${request.mainPlayerID}&FriendPlayerID=${request.mainPlayerID}`);
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
}
