import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { catchError, map, Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  isLogged = false;

  constructor(private http: HttpClient) { }

  login(username: string, password: string) {
    return this.http.post(`/api/users/login`, { username, password })
      .pipe(
        map((response: any) => {
          // save token in local storage if it exists
          if (response && response.token) {
            localStorage.setItem('currentUser', JSON.stringify(response.token));
          }
          this.isLogged = true;
          return response;
        }),
        catchError(this.handleError)  // error handling
      );
  }


  register(user: any) {
    return this.http.post('/api/users/register', user);
    catchError(this.handleError)
  }
  private handleError(error: HttpErrorResponse): Observable<never> {
    let errorMessage = 'Unknown error!';
    if (error.status === 409 || error.status === 401) {
      errorMessage = error.error.message
      console.log(error.error);
    }
    // Handle different HTTP error statuses here...
    return throwError(errorMessage);
  }

  logout() {
    localStorage.removeItem('currentUser');
    this.isLogged = false;
  }

  isLoggedIn() {
    return this.isLogged;
  }


}
