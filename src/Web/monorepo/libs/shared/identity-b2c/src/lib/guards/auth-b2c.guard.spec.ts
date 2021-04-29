import { TestBed } from '@angular/core/testing';

import { AuthB2cGuard } from './auth-b2c.guard';

describe('AuthB2cGuard', () => {
  let guard: AuthB2cGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(AuthB2cGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
