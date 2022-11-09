import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IconRequest, IconResponse } from '../_models/Icon';

@Injectable({
  providedIn: 'root'
})
export class IconService {

  private url: string = environment.ApiUrl + "/icon";

  constructor(private http: HttpClient) { }

  public getAll(): Observable<IconResponse[]> {
    return this.http.get<IconResponse[]>(this.url);
  }

  public getById(id: number): Observable<IconResponse> {
    return this.http.get<IconResponse>(`${this.url}/${id}`);
  }

  public create(request: IconRequest): Observable<IconResponse> {
    return this.http.post<IconResponse>(this.url, request);
  }

  public update(id: number, request: IconRequest): Observable<IconResponse> {
    return this.http.put<IconResponse>(`${this.url}/${id}`, request);
  }

  public delete(id: number): Observable<IconResponse> {
    return this.http.delete<IconResponse>(`${this.url}/${id}`);
  }

}
