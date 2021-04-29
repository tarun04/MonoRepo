import { NgModule } from '@angular/core';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { Routes, RouterModule } from '@angular/router';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';

import {
  ConfigFactory,
  ConfigServiceProvider,
} from '@monorepo/shared/app-config';
import { IdentityB2bModule } from '@monorepo/shared/identity-b2b';
import { SidenavService } from '@monorepo/shared/services';
import { SharedUiModule } from '@monorepo/shared/ui';
import { AppComponent } from './app.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'home',
  },
  {
    path: 'home',
    loadChildren: () =>
      import('@monorepo/application-b2b/home').then(
        (module) => module.HomeModule
      ),
  },
  {
    path: 'dashboard',
    loadChildren: () =>
      import('@monorepo/application-b2b/dashboard').then(
        (module) => module.DashboardModule
      ),
  },
];

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    RouterModule.forRoot(routes),
    MatSidenavModule,
    MatToolbarModule,
    SharedUiModule,
    IdentityB2bModule,
    EffectsModule.forRoot([]),
    StoreModule.forRoot({}),
    StoreDevtoolsModule.instrument({
      maxAge: 25, // Retains last 25 states
      logOnly: ConfigFactory().production, // Restrict extension to log-only mode
    }),
  ],
  providers: [ConfigServiceProvider, SidenavService],
  bootstrap: [AppComponent],
})
export class AppModule {}
