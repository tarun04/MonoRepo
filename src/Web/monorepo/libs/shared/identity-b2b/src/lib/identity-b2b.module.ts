import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { APP_INITIALIZER, Inject, NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { NxModule } from '@nrwl/angular';
import {
  AuthModule,
  AuthWellKnownEndpoints,
  OidcConfigService,
  OidcSecurityService,
  OpenIdConfiguration,
  LogLevel,
} from 'angular-auth-oidc-client';

import { APP_CONFIG, InjectableConfig } from '@monorepo/shared/app-config';
import { CustomStorage } from '@monorepo/shared/framework/storage';
import * as fromReducer from './+state/auth.reducer';
import { AuthEffects } from './+state/auth.effects';
import { SigninOidcComponent } from './containers/signin-oidc/signin-oidc.component';
import { SilentRenewComponent } from './containers/silent-renew/silent-renew.component';

export function configureAuth(
  oidcConfigService: OidcConfigService,
  depProvider: IdentityB2bModule
) {
  const appConfig: InjectableConfig = depProvider.appConfig;
  const passedConfig: OpenIdConfiguration = {
    stsServer: appConfig.stsServer,
    // authWellknownEndpoint: '',
    redirectUrl: `${appConfig.baseUrl}/${appConfig.redirectPath}`,
    clientId: appConfig.clientId,
    responseType: appConfig.responseType,
    scope: appConfig.scopes,
    // hdParam: '',
    postLogoutRedirectUri: appConfig.baseUrl,
    // startCheckSession: false,
    silentRenew: appConfig.silentRenew,
    silentRenewUrl: appConfig.silentRenewUrl,
    // silentRenewTimeoutInSeconds: 0,
    renewTimeBeforeTokenExpiresInSeconds: 10,
    // useRefreshToken: false,
    // usePushedAuthorisationRequests: false,
    // ignoreNonceAfterRefresh: false,
    // postLoginRoute: '',
    // forbiddenRoute: '',
    // unauthorizedRoute: '',
    // autoUserinfo: false,
    // renewUserInfoAfterTokenRenew: false,
    // autoCleanStateAfterAuthentication: false,
    // triggerAuthorizationResultEvent: false,
    logLevel: appConfig.debug ? LogLevel.Debug : LogLevel.None,
    // issValidationOff: false,
    historyCleanupOff: true,
    // maxIdTokenIatOffsetAllowedInSeconds: 0,
    disableIatOffsetValidation: true,
    storage: localStorage,
    // customParams: {},
    // customTokenParams: {},
    // eagerLoadAuthWellKnownEndpoints: false,
    // disableRefreshIdTokenAuthTimeValidation: false,
    // tokenRefreshInSeconds: 0,
    // secureRoutes: [],
    // refreshTokenRetryInSeconds: 0,
    // ngswBypass: false,
  };

  const passedAuthWellKnownEndpoints: AuthWellKnownEndpoints = {
    issuer: appConfig.stsServer,
    jwksUri: `${appConfig.stsServer}/.well-known/openid-configuration/jwks`,
    authorizationEndpoint: `${appConfig.stsServer}/connect/authorize`,
    tokenEndpoint: `${appConfig.stsServer}/connect/token`,
    userinfoEndpoint: `${appConfig.stsServer}/connect/userinfo`,
    endSessionEndpoint: `${appConfig.stsServer}/connect/endsession`,
    checkSessionIframe: `${appConfig.stsServer}/connect/checksession`,
    revocationEndpoint: `${appConfig.stsServer}/connect/revocation`,
    introspectionEndpoint: `${appConfig.stsServer}/connect/introspect`,
    // parEndpoint: '',
  };

  return () =>
    oidcConfigService.withConfig(passedConfig, passedAuthWellKnownEndpoints);
}

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    NxModule.forRoot(),
    HttpClientModule,
    StoreModule.forFeature(fromReducer.AUTH_FEATURE_KEY, fromReducer.reducer, {
      initialState: fromReducer.initialState,
    }),
    EffectsModule.forFeature([AuthEffects]),
    StoreDevtoolsModule.instrument(),
    AuthModule.forRoot({ storage: CustomStorage }),
  ],
  providers: [
    AuthEffects,
    OidcSecurityService,
    OidcConfigService,
    {
      provide: APP_INITIALIZER,
      useFactory: configureAuth,
      deps: [OidcConfigService, IdentityB2bModule],
      multi: true,
    },
  ],
  declarations: [SigninOidcComponent, SilentRenewComponent],
  exports: [AuthModule, SigninOidcComponent, SilentRenewComponent],
})
export class IdentityB2bModule {
  constructor(@Inject(APP_CONFIG) public appConfig: InjectableConfig) {}
}
