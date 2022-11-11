import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, ObservableInput } from 'rxjs';
import { observableToBeFn } from 'rxjs/internal/testing/TestScheduler';
import { environment } from 'src/environments/environment';
import { DirectTransactionResponse, StaticTransactionResponse, TransactionRequest } from '../_models/Transaction';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {

	private url: string = environment.ApiUrl + "/Transaction";

  	constructor(private http: HttpClient) { }

	public getAll(): Observable<StaticTransactionResponse[]>
	{
		return this.http.get<StaticTransactionResponse[]>(this.url).pipe(catchError<StaticTransactionResponse[],ObservableInput<StaticTransactionResponse[]>>(Response=>{
			var a: StaticTransactionResponse[]=[]
			 	  if(Response == null)
			 	  {
			 		  return a
					
			 	  }
			 	return Response
			   }))
	}

	public getById(id: number): Observable<DirectTransactionResponse>
	{
	  return this.http.get<DirectTransactionResponse>(`${this.url}/${id}`);
	}

	public create(request: TransactionRequest): Observable<DirectTransactionResponse>
	{
	  return this.http.post<DirectTransactionResponse>(this.url, request);
	}

	public update(id: number, request: TransactionRequest): Observable<DirectTransactionResponse>
	{
	  return this.http.put<DirectTransactionResponse>(`${this.url}/${id}`, request);
	}

	public delete(id: number): Observable<DirectTransactionResponse>
	{
	  return this.http.delete<DirectTransactionResponse>(`${this.url}/${id}`);
	}
}
