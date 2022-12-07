import { StaticSchoolCategoryResponse, DirectSchoolCategoryResponse, SchoolCategoryRequest } from './../_models/SchoolCategory/index';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SchoolCategoryService {

  constructor(private http: HttpClient) { }

  private url: string = environment.ApiUrl + "/SchoolCategory";

  public getAll(): Observable<StaticSchoolCategoryResponse[]> {
    return this.http.get<StaticSchoolCategoryResponse[]>(this.url);
  }

  public getById(id: number): Observable<DirectSchoolCategoryResponse> {
    return this.http.get<DirectSchoolCategoryResponse>(`${this.url}/${id}`);
  }

  public create(request: SchoolCategoryRequest): Observable<DirectSchoolCategoryResponse> {
    return this.http.post<DirectSchoolCategoryResponse>(this.url, request);
  }

  public update(id: number, request: SchoolCategoryRequest): Observable<DirectSchoolCategoryResponse> {
    return this.http.put<DirectSchoolCategoryResponse>(`${this.url}/${id}`, request);
  }

  public delete(id: number): Observable<DirectSchoolCategoryResponse> {
    return this.http.delete<DirectSchoolCategoryResponse>(`${this.url}/${id}`);
  }
}
