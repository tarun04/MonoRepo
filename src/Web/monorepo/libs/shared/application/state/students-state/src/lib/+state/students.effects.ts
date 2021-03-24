import { Injectable } from '@angular/core';
import { createEffect, Actions, ofType } from '@ngrx/effects';
import { fetch } from '@nrwl/angular';

import * as StudentsFeature from './students.reducer';
import * as StudentsActions from './students.actions';

@Injectable()
export class StudentsEffects {
  init$ = createEffect(() =>
    this.actions$.pipe(
      ofType(StudentsActions.init),
      fetch({
        run: (action) => {
          // Your custom service 'load' logic goes here. For now just return a success action...
          return StudentsActions.loadStudentsSuccess({ students: [] });
        },

        onError: (action, error) => {
          console.error('Error', error);
          return StudentsActions.loadStudentsFailure({ error });
        },
      })
    )
  );

  constructor(private actions$: Actions) {}
}
