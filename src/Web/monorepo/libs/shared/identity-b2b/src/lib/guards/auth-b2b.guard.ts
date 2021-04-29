import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  CanLoad,
  Route,
  Router,
  RouterStateSnapshot,
  UrlSegment,
  UrlTree,
} from '@angular/router';
import { Store } from '@ngrx/store';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { State } from '../+state/auth.reducer';
import * as fromActions from '../+state/auth.actions';

@Injectable({
  providedIn: 'root',
})
export class AuthB2bGuard implements CanActivate, CanLoad {
  constructor(
    private oidcSecurityService: OidcSecurityService,
    private router: Router,
    private store: Store<State>
  ) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | boolean
    | UrlTree
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree> {
    return this.checkUser(state.url);
  }

  canLoad(
    route: Route,
    segments: UrlSegment[]
  ):
    | boolean
    | UrlTree
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree> {
    return this.checkUser(route.path);
  }

  /**
   * Checks if user is authenticated, if not dispatches login action to trigger authentication process
   * @param returnUrl route to return to after successful login
   * @returns boolean, whether user is authenticated
   */
  private checkUser(returnUrl: string): Observable<boolean> {
    return this.oidcSecurityService.isAuthenticated$.pipe(
      map((authenticated: boolean) => {
        if (!authenticated) {
          this.store.dispatch(fromActions.login({ path: returnUrl }));
          return false;
        }
        return true;
      })
    );
  }
}
