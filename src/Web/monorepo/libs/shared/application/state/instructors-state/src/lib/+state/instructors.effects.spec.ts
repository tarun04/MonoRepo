import { TestBed, async } from '@angular/core/testing';

import { Observable } from 'rxjs';

import { provideMockActions } from '@ngrx/effects/testing';
import { provideMockStore } from '@ngrx/store/testing';

import { NxModule, DataPersistence } from '@nrwl/angular';
import { hot } from '@nrwl/angular/testing';

import { InstructorsEffects } from './instructors.effects';
import * as InstructorsActions from './instructors.actions';

describe('InstructorsEffects', () => {
  let actions: Observable<any>;
  let effects: InstructorsEffects;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [NxModule.forRoot()],
      providers: [
        InstructorsEffects,
        DataPersistence,
        provideMockActions(() => actions),
        provideMockStore(),
      ],
    });

    effects = TestBed.inject(InstructorsEffects);
  });

  describe('init$', () => {
    it('should work', () => {
      actions = hot('-a-|', { a: InstructorsActions.init() });

      const expected = hot('-a-|', {
        a: InstructorsActions.loadInstructorsSuccess({ instructors: [] }),
      });

      expect(effects.init$).toBeObservable(expected);
    });
  });
});
