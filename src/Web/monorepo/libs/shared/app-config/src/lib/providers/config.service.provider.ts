import { APP_CONFIG, InjectableConfig } from '../config-models/gateway-config';
import { ConfigService } from '../services/config.service';

export const ConfigFactory = () => {
  // Create env
  const config = {} as InjectableConfig;

  // Read environment variables from browser window
  const browserWindow = window || {};
  const browserWindowEnv = browserWindow['__env'] || {};

  // Assign environment variables from browser window to env
  // In the current implementation, properties from env.js overwrite defaults from the EnvService.
  // If needed, a deep merge can be performed here to merge properties instead of overwriting them.
  for (const key in browserWindowEnv) {
    if (browserWindowEnv.hasOwnProperty(key)) {
      config[key] = window['__env'][key];
    }
  }

  return config;
};

export const ConfigServiceProvider = {
  provide: APP_CONFIG,
  useFactory: ConfigFactory,
  deps: [ConfigService],
};
