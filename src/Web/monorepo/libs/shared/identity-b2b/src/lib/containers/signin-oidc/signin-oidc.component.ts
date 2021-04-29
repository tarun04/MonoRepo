import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { AuthUser } from '@monorepo/shared/data-models';
import { State } from '../../+state/auth.reducer';
import * as fromActions from '../../+state/auth.actions';

@Component({
  selector: 'signin-oidc',
  templateUrl: './signin-oidc.component.html',
  styleUrls: ['./signin-oidc.component.scss'],
})
export class SigninOidcComponent implements OnInit {
  isAuthenticated$: Observable<boolean>;

  constructor(
    private oidcSecurityService: OidcSecurityService,
    private store: Store<State>
  ) {}

  ngOnInit(): void {
    this.isAuthenticated$ = this.oidcSecurityService.isAuthenticated$;

    this.oidcSecurityService.checkAuth().subscribe((isAuthenticated) => {
      if (isAuthenticated) {
        this.oidcSecurityService.userData$.pipe(
          map((user) => {
            const authUser = user as AuthUser;
            authUser.idToken = this.oidcSecurityService.getIdToken();

            this.store.dispatch(
              fromActions.loginSuccess({ authUser: authUser })
            );

            console.log('app authenticated', isAuthenticated);
          })
        );
      } else {
        this.store.dispatch(
          fromActions.loginFailure({ error: 'app authentication failed' })
        );
      }
    });
  }
}
