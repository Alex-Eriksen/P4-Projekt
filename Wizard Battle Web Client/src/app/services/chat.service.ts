import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Friend } from '../_models/Friend/Friend';
import { StaticFriendshipResponse } from '../_models/Friendship';
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


  public GetAll(playerId: number): Observable<StaticFriendshipResponse[]> {
    return this.http.get<StaticFriendshipResponse[]>(`${this.url}/${playerId}`);
  }
}
