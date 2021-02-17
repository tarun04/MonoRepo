import { Component, Input, OnInit } from '@angular/core';
import { SidenavService } from '@monorepo/shared/services';

@Component({
  selector: 'header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit {
  @Input() title: string;

  constructor(private sidenavService: SidenavService) {}

  ngOnInit(): void {}

  toggleSidenav(): void {
    this.sidenavService.toggle();
  }
}
