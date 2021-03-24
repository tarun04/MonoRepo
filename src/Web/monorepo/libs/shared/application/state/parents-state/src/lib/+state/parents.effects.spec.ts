import { TestBed, async } from '@angular/core/testing';

import { Observable } from 'rxjs';

import { provideMockActions } from '@ngrx/effects/testing';
import { provideMockStore } from '@ngrx/store/testing';

import { NxModule, DataPersistence } from '@nrwl/angular';
import { hot } from '@nrwl/angular/testing';

import { ParentsEffects } from './parents.effects';
import * as ParentsActions from './parents.actions';

describe('ParentsEffects', () => {
  let actions: Observable<any>;
  let effects: ParentsEffects;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [NxModule.forRoot()],
      providers: [
        ParentsEffects,
        DataPersistence,
        provideMockActions(() => actions),
        provideMockStore(),
      ],
    });

    effects = TestBed.inject(ParentsEffects);
  });

  describe('init$', () => {
    it('should work', () => {
      actions = hot('-a-|', { a: ParentsActions.init() });

      const expected = hot('-a-|', {
        a: ParentsActions.loadParentsSuccess({ parents: [] }),
      });

      expect(effects.init$).toBeObservable(expected);
    });
  });
});
