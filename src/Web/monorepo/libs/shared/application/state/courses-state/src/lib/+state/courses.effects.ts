import { Injectable } from '@angular/core';
import { createEffect, Actions, ofType } from '@ngrx/effects';
import { fetch } from '@nrwl/angular';

import * as CoursesFeature from './courses.reducer';
import * as CoursesActions from './courses.actions';

@Injectable()
export class CoursesEffects {
  init$ = createEffect(() =>
    this.actions$.pipe(
      ofType(CoursesActions.init),
      fetch({
        run: (action) => {
          // Your custom service 'load' logic goes here. For now just return a success action...
          return CoursesActions.loadCoursesSuccess({ courses: [] });
        },

        onError: (action, error) => {
          console.error('Error', error);
          return CoursesActions.loadCoursesFailure({ error });
        },
      })
    )
  );

  constructor(private actions$: Actions) {}
}
