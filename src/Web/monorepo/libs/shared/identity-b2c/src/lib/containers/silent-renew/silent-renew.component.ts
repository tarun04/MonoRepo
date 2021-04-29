import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'silent-renew',
  templateUrl: './silent-renew.component.html',
  styleUrls: ['./silent-renew.component.scss'],
})
export class SilentRenewComponent implements OnInit {
  constructor() {}

  ngOnInit(): void {
    /* The parent window hosts the Angular application */
    var parent = window.parent;
    /* Send the id_token information to the oidc message handler */
    var event = new CustomEvent('oidc-silent-renew-message', {
      detail: window.location.hash.substr(1),
    });
    parent.dispatchEvent(event);
  }
}
