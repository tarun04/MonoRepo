import { TestBed } from '@angular/core/testing';

import { UserB2cGuard } from './user-b2c.guard';

describe('UserB2cGuard', () => {
  let guard: UserB2cGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(UserB2cGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
