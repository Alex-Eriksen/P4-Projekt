import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { DirectSkinItemResponse, SkinItemRequest, StaticSkinItemResponse } from '../_models/SkinItem';

@Injectable({
  providedIn: 'root'
})
export class SkinService {

	private url: string = environment.ApiUrl + "/SkinItem";

	constructor(private http: HttpClient) { }

  	public getAll(): Observable<StaticSkinItemResponse[]>
	{
		return this.http.get<StaticSkinItemResponse[]>(this.url);
  	}

	public getById(id: number): Observable<DirectSkinItemResponse>
	{
		return this.http.get<DirectSkinItemResponse>(`${this.url}/${id}`);
	}

	public create(request: SkinItemRequest): Observable<DirectSkinItemResponse>
	{
		return this.http.post<DirectSkinItemResponse>(this.url, request);
	}

	public update(id: number, request: SkinItemRequest): Observable<DirectSkinItemResponse>
	{
		return this.http.put<DirectSkinItemResponse>(`${this.url}/${id}`, request);
	}

	public delete(id: number): Observable<DirectSkinItemResponse>
	{
		return this.http.delete<DirectSkinItemResponse>(`${this.url}/${id}`);
	}
}
