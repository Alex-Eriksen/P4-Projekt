import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AccountRequest, DirectAccountResponse, StaticAccountResponse } from '../_models/Account';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private url: string = environment.ApiUrl + "/Account";

  constructor(private http: HttpClient) { }

  public getAll(): Observable<StaticAccountResponse[]>
	{
		return this.http.get<StaticAccountResponse[]>(this.url);
	}

  public getById(accountId: number): Observable<DirectAccountResponse>
  {
    return this.http.get<DirectAccountResponse>(`${this.url}/${accountId}`);
  }

  public update(accountId: number, request: AccountRequest): Observable<DirectAccountResponse>
	{
		return this.http.put<DirectAccountResponse>(`${this.url}/${accountId}`, request);
	}
  public getByToken(token:string): Observable<DirectAccountResponse>
  {
    return this.http.get<DirectAccountResponse>(`${this.url}/getbytoken/${token}`, );
  }
}
