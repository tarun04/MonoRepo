import { TestBed } from '@angular/core/testing';

import { AuthB2bGuard } from './auth-b2b.guard';

describe('AuthB2bGuard', () => {
  let guard: AuthB2bGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(AuthB2bGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
