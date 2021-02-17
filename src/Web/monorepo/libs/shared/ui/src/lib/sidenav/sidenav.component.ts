import { Component, Input, OnInit } from '@angular/core';
import { NavRoutes } from '@monorepo/shared/data-models';
import { SidenavService } from '@monorepo/shared/services';

@Component({
  selector: 'sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.scss'],
})
export class SidenavComponent implements OnInit {
  @Input() routes: NavRoutes[] = [
    {
      subroutes: [
        {
          title: 'Home',
          route: '/home',
          icon: 'home',
          isActive: true,
        },
        {
          title: 'Dashboard',
          route: 'dashboard',
          icon: 'dashboard',
          isActive: true,
        },
      ],
    },
    {
      subheading: 'Favorites',
      subroutes: [
        {
          title: 'All resources',
          route: '/resources',
          icon: 'apps',
          isActive: true,
        },
        {
          title: 'Accounts',
          route: '/accounts',
          icon: 'bubble_chart',
          isActive: true,
        },
        {
          title: 'Security Center',
          route: '/leads',
          icon: 'security',
          isActive: true,
        },
        {
          title: 'Monitor',
          route: '/opportunities',
          icon: 'auto_graph',
          isActive: true,
        },
        {
          title: 'Cost Management',
          route: '/opportunities',
          icon: 'attach_money',
          isActive: true,
        },
      ],
    },
  ];

  constructor(private sidenavService: SidenavService) {}

  ngOnInit(): void {}

  toggleSidenav(): void {
    this.sidenavService.toggle();
  }
}
