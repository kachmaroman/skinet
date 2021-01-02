import { IUser } from './../shared/models/user';
import { ReplaySubject, Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;

  private currentUserSource: ReplaySubject<IUser> = new ReplaySubject<IUser>(1);
  public get currentUser$(): Observable<IUser> {
    return this.currentUserSource.asObservable();
  }

  constructor(
    private http: HttpClient,
    private router: Router) { }

  loadCurrentUser(token: string): Observable<any> {
    if (token === null) {
      this.currentUserSource.next(null);
      return of(null);
    }

    let headers: HttpHeaders = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);

    return this.http.get<IUser>(this.baseUrl + 'account', { headers }).pipe(map<IUser, any>(user => {
      if (user) {
        localStorage.setItem('token', user.token);
        this.currentUserSource.next(user);
      }
    }));
  }

  login(values: any): Observable<any> {
    return this.http.post<IUser>(this.baseUrl + 'account/login', values).pipe(map<IUser, any>(user => {
      if (user) {
        localStorage.setItem('token', user.token);
        this.currentUserSource.next(user);
      }
    }));
  }

  register(values: any): Observable<IUser> {
    return this.http.post<IUser>(this.baseUrl + 'account/register', values).pipe(map<IUser, any>(user => {
      if (user) {
        localStorage.setItem('token', user.token);
        this.currentUserSource.next(user);
      }
    }))
  }

  logout(): void {
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }

  checkEmailExists(email: string): Observable<boolean> {
    return this.http.get<boolean>(this.baseUrl + `account/emailexists?email=${email}`);
  }
}
