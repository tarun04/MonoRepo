import { APP_INITIALIZER, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ConfigService } from './services/config.service';
import { ConfigFactory } from './providers/config.service.provider';

@NgModule({
  imports: [CommonModule],
  providers: [
    ConfigService,
    {
      provide: APP_INITIALIZER,
      useFactory: ConfigFactory,
      deps: [ConfigService],
    },
  ],
})
export class AppConfigModule {}
