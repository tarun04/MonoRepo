import { Injectable } from '@angular/core';
import { createEffect, Actions, ofType } from '@ngrx/effects';
import { fetch } from '@nrwl/angular';

import * as ParentsFeature from './parents.reducer';
import * as ParentsActions from './parents.actions';

@Injectable()
export class ParentsEffects {
  init$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ParentsActions.init),
      fetch({
        run: (action) => {
          // Your custom service 'load' logic goes here. For now just return a success action...
          return ParentsActions.loadParentsSuccess({ parents: [] });
        },

        onError: (action, error) => {
          console.error('Error', error);
          return ParentsActions.loadParentsFailure({ error });
        },
      })
    )
  );

  constructor(private actions$: Actions) {}
}
