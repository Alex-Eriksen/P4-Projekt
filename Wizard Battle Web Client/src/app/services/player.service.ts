import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, Subject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { DirectPlayerResponse, PlayerRequest, StaticPlayerResponse } from '../_models/Player';
import { PlayerAccountRequest } from '../_models/Player/PlayerAccountRequest';

@Injectable({
  providedIn: 'root'
})
export class PlayerService {

  private url: string = environment.ApiUrl + "/Player";

  public currentStatusSubject: Subject<string>;
  public OnStatusChanged: Observable<string>;

  constructor(private http: HttpClient) {
    this.currentStatusSubject = new Subject<string>();
    this.OnStatusChanged = this.currentStatusSubject.asObservable();
  }

  public getAll(): Observable<StaticPlayerResponse[]>
  {
    return this.http.get<StaticPlayerResponse[]>(this.url);
  }

  public getById(playerId: number): Observable<DirectPlayerResponse>
  {
    return this.http.get<DirectPlayerResponse>(`${this.url}/${playerId}`);
  }

  public create(request: PlayerAccountRequest): Observable<DirectPlayerResponse>
  {
    return this.http.post<DirectPlayerResponse>(this.url, request);
  }

  public update(playerId: number, request: PlayerRequest): Observable<DirectPlayerResponse>
  {
    return this.http.put<DirectPlayerResponse>(`${this.url}/${playerId}`, request);
  }

  public changeStatus(playerId: number, status: string): Observable<DirectPlayerResponse>
  {
    return this.http.put<DirectPlayerResponse>(`${this.url}/status?playerId=${playerId}&status=${status}`, null).pipe(map(data => {
      this.currentStatusSubject.next(data.playerStatus);
      console.log("Status changed");
      return data;
    }));
  }
}
