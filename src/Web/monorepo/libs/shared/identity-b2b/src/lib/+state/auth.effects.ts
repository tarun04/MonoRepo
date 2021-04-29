import { Inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { createEffect, Actions, ofType } from '@ngrx/effects';
import { fetch } from '@nrwl/angular';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { map, take } from 'rxjs/operators';

import { APP_CONFIG, InjectableConfig } from '@monorepo/shared/app-config';
import * as fromReducer from './auth.reducer';
import * as fromActions from './auth.actions';

@Injectable()
export class AuthEffects {
  login$ = createEffect(() =>
    this.actions$.pipe(
      ofType(fromActions.login),
      fetch({
        run: (action, state: fromReducer.AuthPartialState) => {
          if (
            !state[fromReducer.AUTH_FEATURE_KEY].loaded &&
            !state[fromReducer.AUTH_FEATURE_KEY].authenticated
          ) {
            localStorage.setItem(
              'returnUrl',
              `${this.appConfig.baseUrl}/${action.path}`
            );
            return this.oidcSecurityService.userData$.pipe(
              take(1),
              map((userData) => {
                if (userData)
                  return fromActions.loginSuccess({ authUser: userData });
                else {
                  this.oidcSecurityService.authorize();
                  return fromActions.loginInProgress();
                }
              })
            );
          }
        },

        onError: (action, error) => {
          console.error('Error', error);
          return fromActions.loginFailure({ error });
        },
      })
    )
  );

  loginSuccess$ = createEffect(() =>
    this.actions$.pipe(
      ofType(fromActions.loginSuccess),
      fetch({
        run: (action) => {
          setTimeout(() => {
            let returnUrl = localStorage
              .getItem('returnUrl')
              .replace(`${window.location.origin}/`, '');
            const removeString = returnUrl.slice(0, returnUrl.indexOf('/'));
            returnUrl = returnUrl.replace(removeString, '');
            this.router.navigateByUrl(returnUrl);
          }, 0);
        },

        onError: (action, error) => {
          console.error('Error', error);
          return fromActions.loginFailure({ error });
        },
      })
    )
  );

  logout$ = createEffect(() =>
    this.actions$.pipe(
      ofType(fromActions.logout),
      fetch({
        run: (action) => {
          this.oidcSecurityService.logoff();
        },

        onError: (action, error) => {
          console.error('Error', error);
          return fromActions.logoutFailure({ error });
        },
      })
    )
  );

  loadAuthUser$ = createEffect(() =>
    this.actions$.pipe(
      ofType(fromActions.loadAuthUser),
      fetch({
        run: (action) => {
          return this.oidcSecurityService.userData$.pipe(
            take(1),
            map((userData) => {
              if (userData)
                return fromActions.loadAuthUserSuccess({
                  authUser: userData,
                });
              else {
                this.oidcSecurityService.authorize();
                return fromActions.loginInProgress();
              }
            })
          );
        },

        onError: (action, error) => {
          console.error('Error', error);
          return fromActions.loadAuthUserFailure({ error });
        },
      })
    )
  );

  constructor(
    private actions$: Actions,
    @Inject(APP_CONFIG) private appConfig: InjectableConfig,
    private oidcSecurityService: OidcSecurityService,
    private router: Router
  ) {}
}
