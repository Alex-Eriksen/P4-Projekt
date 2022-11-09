import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { catchError, EMPTY, Observable } from 'rxjs';
import { AuthenticationService } from '../services/authentication.service';
import { environment } from 'src/environments/environment';

@Injectable()
export class AuthenticationInterceptor implements HttpInterceptor
{
	private isLoggedIn: boolean = false;

	constructor(private authenticationService: AuthenticationService)
	{
		this.authenticationService.OnTokenChanged.subscribe((token) =>
		{
			if (token !== '')
			{
				this.isLoggedIn = true;
			}
			else
			{
				this.isLoggedIn = false;
			}
		});
  	}

	intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>>
	{
		const accessToken: string = this.authenticationService.AccessToken;
		const isApiUrl: boolean = request.url.startsWith(environment.ApiUrl);
		if (this.isLoggedIn && isApiUrl)
		{
			request = request.clone({
				setHeaders: { Authorization: `bearer ${accessToken}` }
			});
		}
    	return next.handle(request).pipe(
        catchError((error: HttpErrorResponse) => {
        if (error.error instanceof Error) {
          // A client-side or network error occurred. Handle it accordingly.
          console.error('An error occurred:', error.error.message);
        } else {
          // The backend returned an unsuccessful response code.
          // The response body may contain clues as to what went wrong,
          console.error(`Backend returned code ${error.status}, body was: ${error.error}`);
        }
        return EMPTY;
      }));
  	}
}
