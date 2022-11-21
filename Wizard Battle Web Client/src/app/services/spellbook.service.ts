import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { DirectSpellBookResponse, SpellBookRequest, StaticSpellBookResponse } from '../_models/SpellBook';

@Injectable({
  providedIn: 'root'
})
export class SpellbookService {

	private url: string = environment.ApiUrl + "/SpellBook";

	constructor(private http: HttpClient) { }

	public getAll(): Observable<StaticSpellBookResponse[]> {
		return this.http.get<StaticSpellBookResponse[]>(this.url);
	}

	public getById(id: number): Observable<DirectSpellBookResponse> {
		return this.http.get<DirectSpellBookResponse>(`${this.url}/${id}`);
	}

	public create(request: SpellBookRequest): Observable<DirectSpellBookResponse> {
		return this.http.post<DirectSpellBookResponse>(this.url, request);
	}

	public update(id: number, request: SpellBookRequest): Observable<DirectSpellBookResponse> {
		return this.http.put<DirectSpellBookResponse>(`${this.url}/${id}`, request);
	}

	public delete(id: number): Observable<DirectSpellBookResponse> {
		return this.http.get<DirectSpellBookResponse>(`${this.url}/${id}`);
	}
}
