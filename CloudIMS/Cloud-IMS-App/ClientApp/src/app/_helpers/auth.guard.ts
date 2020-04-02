import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { UserAuthorizationService } from '../services/UserAuthorization.service';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
  constructor(
    private router: Router,
    private auth: UserAuthorizationService
  ) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {

    let isLogin = this.auth.isLoggedIn()

    if (!isLogin) {
      console.log({ module: "Auth Guard", isLogin: isLogin })

      // not logged in so redirect to login page with the return url
      this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
      return false;
    }

    // user is logged in
    return true;
  }
}
