import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import {AuthService} from "../services/auth.service";
import {JwtHelperService} from "@auth0/angular-jwt";
@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(
    private router: Router,
    private jwtHelper: JwtHelperService,
    private authService: AuthService
  ) {}

  async canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const token = localStorage.getItem('jwt');

    if (token && !this.jwtHelper.isTokenExpired(token)) {
      console.log(this.jwtHelper.decodeToken(token));
      return true;
    }

    const refreshToken = localStorage.getItem('refreshToken');
    if (!refreshToken) {
      this.router.navigate(['auth']);
      return false;
    }

    const isRefreshSuccess = await this.authService.tryRefreshingTokens(token!, refreshToken);
    if (!isRefreshSuccess) {
      this.router.navigate(['auth']);
    }

    return isRefreshSuccess;
  }
}
