(function (window) {
  window.__env = window.__env || {};
  // API url
  window.__env.environmentName = '${ENV}';

  window.__env.production = true;
  window.__env.stsServer = '${STS_SERVER}';
  window.__env.baseUrl = '${BASE_URL}';
  window.__env.alertUrl = '${ALERT_URL}';
  window.__env.redirectPath = 'signin-oidc';
  window.__env.clientId = 'application-angular';
  window.__env.scopes = 'openid profile email application';
  window.__env.responseType = 'code';
  window.__env.debug = true;
  window.__env.silentRenew = false;
  window.__env.silentRenewUrl = '${SILENT_RENEW_URL}';
  window.__env.devTenantName = '${TENANT_NAME}';
  window.__env.appInsights_instrumentationKey = '${INSTRUMENTATION_KEY}';
  window.__env.loggingThreshold = 0;
  window.__env.gatewayHost = '${GATEWAY_HOST}';
})(this);
