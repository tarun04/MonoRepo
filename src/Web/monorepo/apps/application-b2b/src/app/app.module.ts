import { NgModule } from '@angular/core';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { Routes, RouterModule } from '@angular/router';

import { ConfigServiceProvider } from '@monorepo/shared/app-config';
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
  ],
  providers: [ConfigServiceProvider, SidenavService],
  bootstrap: [AppComponent],
})
export class AppModule {}
