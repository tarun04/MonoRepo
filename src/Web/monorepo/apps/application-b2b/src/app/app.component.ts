import { Component, ViewChild } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';

import { SidenavService } from '@monorepo/shared/services';

@Component({
  selector: 'application-b2b-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'application-b2b';
  @ViewChild('sidenav') sidenav: MatSidenav;

  constructor(private sidenavService: SidenavService) {}

  ngOnInit(): void {}

  ngAfterViewInit(): void {
    this.sidenavService.setSidenav(this.sidenav);
  }
}
