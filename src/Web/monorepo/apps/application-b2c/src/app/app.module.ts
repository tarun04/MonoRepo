import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';

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
      import('@monorepo/application-b2c/home').then(
        (module) => module.HomeModule
      ),
  },
  {
    path: 'dashboard',
    loadChildren: () =>
      import('@monorepo/application-b2c/dashboard').then(
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
  providers: [SidenavService],
  bootstrap: [AppComponent],
})
export class AppModule {}
