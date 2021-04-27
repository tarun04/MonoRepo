import { Injectable } from '@angular/core';
import { InjectableConfig } from '../config-models/gateway-config';

@Injectable({
  providedIn: 'root',
})
export class ConfigService {
  public config: InjectableConfig;

  constructor() {}
}
