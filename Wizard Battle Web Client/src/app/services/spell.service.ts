import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { DirectSpellResponse, SpellRequest, StaticSpellResponse } from '../_models/Spell';

@Injectable({
  providedIn: 'root'
})
export class SpellService {

	private url: string = environment.ApiUrl + "/Spell";

	constructor(private http: HttpClient) { }

	public getAll(): Observable<StaticSpellResponse[]> {
		return this.http.get<StaticSpellResponse[]>(this.url);
	}

	public getById(id: number): Observable<DirectSpellResponse> {
		return this.http.get<DirectSpellResponse>(`${this.url}/${id}`);
	}

	public create(request: SpellRequest): Observable<DirectSpellResponse> {
		return this.http.post<DirectSpellResponse>(this.url, request);
	}

	public update(id: number, request: SpellRequest): Observable<DirectSpellResponse> {
		return this.http.put<DirectSpellResponse>(`${this.url}/${id}`, request);
	}

	public delete(id: number): Observable<DirectSpellResponse> {
		return this.http.delete<DirectSpellResponse>(`${this.url}/${id}`);
	}
}
