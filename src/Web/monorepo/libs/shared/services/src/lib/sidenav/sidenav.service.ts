import { Injectable } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';

@Injectable()
export class SidenavService {
  private sidenav: MatSidenav;

  constructor() {}

  setSidenav(sidenav: MatSidenav): void {
    this.sidenav = sidenav;
  }

  public toggle() {
    this.sidenav.toggle();
  }
}
