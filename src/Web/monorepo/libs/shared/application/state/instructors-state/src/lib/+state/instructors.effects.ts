import { Injectable } from '@angular/core';
import { createEffect, Actions, ofType } from '@ngrx/effects';
import { fetch } from '@nrwl/angular';

import * as InstructorsFeature from './instructors.reducer';
import * as InstructorsActions from './instructors.actions';

@Injectable()
export class InstructorsEffects {
  init$ = createEffect(() =>
    this.actions$.pipe(
      ofType(InstructorsActions.init),
      fetch({
        run: (action) => {
          // Your custom service 'load' logic goes here. For now just return a success action...
          return InstructorsActions.loadInstructorsSuccess({ instructors: [] });
        },

        onError: (action, error) => {
          console.error('Error', error);
          return InstructorsActions.loadInstructorsFailure({ error });
        },
      })
    )
  );

  constructor(private actions$: Actions) {}
}
