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

  public profileUpdateSubject: Subject<any>;
  public OnProfileUpdated: Observable<any>;

  constructor(private http: HttpClient) {
    this.currentStatusSubject = new Subject<string>();
    this.OnStatusChanged = this.currentStatusSubject.asObservable();

	this.profileUpdateSubject = new Subject<any>();
	this.OnProfileUpdated = this.profileUpdateSubject.asObservable();

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
	this.profileUpdateSubject.next("");
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

  	public toHoursAndMinutes(totalMinutes: number): string {
		let hours = Math.floor(totalMinutes / 60);
	  	let minutes = totalMinutes % 60;
	  	return `${hours}h${minutes}m`;
	}

	  // Calculates level of player
	  getLevel(playerExp: number): number {
		let baseExp = 25;
		let playerLvl = 1;
		while(playerExp > baseExp) {
		  if(playerExp >= baseExp) { // if player experience is higher than level threshold
			playerExp = Math.round(playerExp - baseExp); // Subtracts points used for level up
			playerLvl++; // Level up
			baseExp = Math.round(baseExp * 1.2); // Increments base experience points
		  }
		}
		return playerLvl; // Assings local variable current player experience
	  }
}
