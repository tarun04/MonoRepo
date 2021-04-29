import { Injectable } from '@angular/core';
import { AbstractSecurityStorage } from 'angular-auth-oidc-client';

@Injectable()
export class CustomStorage implements AbstractSecurityStorage {
  read(key: string) {
    return JSON.parse(localStorage.getItem(key));
  }
  write(key: string, value: any): void {
    localStorage.setItem(key, JSON.stringify(value));
  }
  remove(key: string): void {
    localStorage.removeItem(key);
  }
}
