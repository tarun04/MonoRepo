import { InjectionToken } from '@angular/core';

export interface GatewayConfig {
  // The values that are defined here are the default values that can
  // be overridden by config.js
  environmentName: string;

  // Whether or not to enable production mode
  production: boolean;
  // Identity url
  stsServer: string;
  // API url
  baseUrl: string;
  alertUrl: string;
  redirectPath: string;
  clientId: string;
  scopes: string;
  responseType: string;
  // Whether or not to enable debug mode
  debug: boolean;
  silentRenew: boolean;
  silentRenewUrl: string;
  devTenantName: string;
  appInsights_instrumentationKey: string;
  loggingThreshold: string;
  gatewayHost: string;
}

export const APP_CONFIG = new InjectionToken<InjectableConfig>('AppConfig');

// export const environment: InjectableConfig = {
//   gatewayHost: '',
// };

export class InjectableConfig {
  // The values that are defined here are the default values that can
  // be overridden by config.js
  environmentName: string;

  // Whether or not to enable production mode
  production: boolean;
  // Identity url
  stsServer: string;
  // API url
  baseUrl: string;
  alertUrl: string;
  redirectPath: string;
  clientId: string;
  scopes: string;
  responseType: string;
  // Whether or not to enable debug mode
  debug: boolean;
  silentRenew: boolean;
  silentRenewUrl: string;
  devTenantName: string;
  appInsights_instrumentationKey: string;
  loggingThreshold: string;
  gatewayHost: string;
}
