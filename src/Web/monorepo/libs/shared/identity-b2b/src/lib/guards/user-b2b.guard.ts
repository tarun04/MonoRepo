import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  CanLoad,
  Route,
  RouterStateSnapshot,
  UrlSegment,
  UrlTree,
} from '@angular/router';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { AuthPartialState } from '../+state/auth.reducer';
import * as fromActions from '../+state/auth.actions';
import * as fromSelectors from '../+state/auth.selectors';

@Injectable({
  providedIn: 'root',
})
export class UserB2bGuard implements CanActivate, CanLoad {
  constructor(private store: Store<AuthPartialState>) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | boolean
    | UrlTree
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree> {
    return this.initUser();
  }

  canLoad(
    route: Route,
    segments: UrlSegment[]
  ):
    | boolean
    | UrlTree
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree> {
    return this.initUser();
  }

  /**
   * Checks if user is loaded, if not dispatches loadAuthUser action to load user
   * @returns boolean, whether user is loaded
   */
  private initUser(): Observable<boolean> {
    return this.store.select(fromSelectors.getAuthUser).pipe(
      map((user) => {
        if (!user) {
          this.store.dispatch(fromActions.loadAuthUser());
          return false;
        }
        return true;
      })
    );
  }
}
