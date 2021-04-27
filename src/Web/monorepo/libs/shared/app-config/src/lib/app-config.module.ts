import { ModuleWithProviders, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { APP_CONFIG, InjectableConfig } from './config-models/gateway-config';

@NgModule({
  imports: [CommonModule],
  providers: [],
})
export class AppConfigModule {
  static forRoot(
    environment: InjectableConfig
  ): ModuleWithProviders<AppConfigModule> {
    return {
      ngModule: AppConfigModule,
      providers: [{ provide: APP_CONFIG, useValue: environment }],
    };
  }
}
