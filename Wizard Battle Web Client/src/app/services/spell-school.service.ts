import { StaticSpellSchoolResponse, DirectSpellSchoolResponse, SpellSchoolRequest } from './../_models/SpellSchool/index';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SpellSchoolService {

	constructor(private http: HttpClient) { }

  	private url: string = environment.ApiUrl + "/SpellSchool";

  	public getAll(): Observable<StaticSpellSchoolResponse[]> {
  	  return this.http.get<StaticSpellSchoolResponse[]>(this.url);
  	}

  	public getById(id: number): Observable<DirectSpellSchoolResponse> {
  	  return this.http.get<DirectSpellSchoolResponse>(`${this.url}/${id}`);
  	}

  	public create(request: SpellSchoolRequest): Observable<DirectSpellSchoolResponse> {
  	  return this.http.post<DirectSpellSchoolResponse>(this.url, request);
  	}

  	public update(id: number, request: SpellSchoolRequest): Observable<DirectSpellSchoolResponse> {
  	  return this.http.put<DirectSpellSchoolResponse>(`${this.url}/${id}`, request);
  	}

  	public delete(id: number): Observable<DirectSpellSchoolResponse> {
  	  return this.http.delete<DirectSpellSchoolResponse>(`${this.url}/${id}`);
  	}
}
