import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { UserAuthorizationService } from '../services/UserAuthorization.service';
import { CookieService } from 'ngx-cookie-service';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
  constructor(
    private router: Router,
    private auth: UserAuthorizationService,
    private cookieService: CookieService
  ) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    let cookieUserId: string = this.cookieService.get('userId');
    let cookieToken: string = this.cookieService.get('token');

    if (!this.auth.isLoggedIn()) {

      // not logged in so redirect to login page with the return url
      this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
      return false;
    }

    // user is logged in
    return true;
  }
}
