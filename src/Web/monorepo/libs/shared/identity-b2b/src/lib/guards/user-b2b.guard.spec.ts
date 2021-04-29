import { TestBed } from '@angular/core/testing';

import { UserB2bGuard } from './user-b2b.guard';

describe('UserB2bGuard', () => {
  let guard: UserB2bGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(UserB2bGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
