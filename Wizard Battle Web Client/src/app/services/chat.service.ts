import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Friend } from '../_models/Friend/Friend';
import { DirectFriendshipResponse, FriendshipRequest, StaticFriendshipResponse } from '../_models/Friendship';
import { MessageRequest } from '../_models/Message';
import { StaticMessageResponse } from '../_models/Message/StaticMessageResponse';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  private FriendSubject: BehaviorSubject<Friend>;
  public OnFriendChanged: Observable<Friend>;
  private url: string = environment.ApiUrl + "/Friendship";

  constructor(private http: HttpClient) {
    this.FriendSubject = new BehaviorSubject<Friend>(new Friend());
    this.OnFriendChanged = this.FriendSubject.asObservable();
  }


  public GetAll(playerId: number): Observable<DirectFriendshipResponse[]> { // Gets all friendship displayed in the friendlist
    return this.http.get<DirectFriendshipResponse[]>(`${this.url}/${playerId}`);
  }

  public Create(request: FriendshipRequest): Observable<DirectFriendshipResponse> { //
    return this.http.post<DirectFriendshipResponse>(`${this.url}`, request)
  }

  public Update(request: FriendshipRequest): Observable<DirectFriendshipResponse> {
    return this.http.put<DirectFriendshipResponse>(`${this.url}`, request);
  }

  public Delete(request: FriendshipRequest): Observable<DirectFriendshipResponse> {
    return this.http.delete<DirectFriendshipResponse>(`${this.url}?MainPlayerID=${request.mainPlayerID}&FriendPlayerID=${request.mainPlayerID}`);
  }

  public GetAllMessages(playerId: number, friendId: number): Observable<StaticMessageResponse[]> {
    return this.http.get<StaticMessageResponse[]>(`${this.url}/messages?mainPlayerId=${playerId}&friendPlayerId=${friendId}`);
  }

  public SendMessage(request: MessageRequest): Observable<StaticMessageResponse> {
    return this.http.post<StaticMessageResponse>(`${this.url}/messages`, request);
  }
}
