// HYBRID

// (function (window) {
//   window.__env = window.__env || {};

//   window.__env.environmentName = 'DEVELOPMENT';
//   window.__env.production = true;
//   window.__env.stsServer = 'https://identity-b2b';
//   window.__env.baseUrl = 'http://application-b2b';
//   window.__env.alertUrl = 'https://alerts/signalr';
//   window.__env.redirectPath = 'signin-oidc';
//   window.__env.clientId = 'application-angular';
//   window.__env.scopes = 'openid profile email application';
//   window.__env.responseType = 'code';
//   window.__env.debug = true;
//   window.__env.silentRenew = false;
//   window.__env.silentRenewUrl = 'https://application-b2b/silent-renew';
//   window.__env.devTenantName = 'default';
//   window.__env.appInsights_instrumentationKey =
//     'bf1c22fa-dcae-4dff-ac41-96b58e330997';
//   window.__env.loggingThreshold = 0;
//   window.__env.gatewayHost = 'https://application-b2b-gw';
// })(this);

// LOCAL
//
(function (window) {
  window.__env = window.__env || {};

  window.__env.environmentName = 'DEVELOPMENT';
  window.__env.production = true;
  window.__env.stsServer = 'https://localhost:5001';
  window.__env.baseUrl = 'http://localhost:4200/application';
  window.__env.alertUrl = 'http://localhost:5026/alerts/signalr';
  window.__env.redirectPath = 'signin-oidc';
  window.__env.clientId = 'application-angular';
  window.__env.scopes = 'openid profile email application';
  window.__env.responseType = 'code';
  window.__env.debug = true;
  window.__env.silentRenew = false;
  window.__env.silentRenewUrl =
    'http://localhost:4200/application/silent-renew';
  window.__env.devTenantName = 'localhost';
  window.__env.appInsights_instrumentationKey =
    'bf1c22fa-dcae-4dff-ac41-96b58e330997';
  window.__env.loggingThreshold = 0;
  window.__env.gatewayHost = 'https://localhost:5559';
})(this);
