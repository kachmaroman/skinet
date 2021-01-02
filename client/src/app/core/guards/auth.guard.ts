import { IUser } from './../../shared/models/user';
import { AccountService } from './../../account/account.service';
import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router, ActivatedRoute } from '@angular/router';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(
    private router: Router,
    private accountService: AccountService) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> {
    return this.accountService.currentUser$.pipe(map<IUser, any>(auth => {
      console.log('auth ', auth);
      return auth ? true : this.router.navigate(['account/login'], { queryParams: { returnUrl: state.url } });
    }));
  }
}
