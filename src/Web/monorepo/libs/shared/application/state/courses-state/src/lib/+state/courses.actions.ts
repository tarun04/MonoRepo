import { createAction, props } from '@ngrx/store';
import { CoursesEntity } from './courses.models';

export const init = createAction('[Courses Page] Init');

export const loadCoursesSuccess = createAction(
  '[Courses/API] Load Courses Success',
  props<{ courses: CoursesEntity[] }>()
);

export const loadCoursesFailure = createAction(
  '[Courses/API] Load Courses Failure',
  props<{ error: any }>()
);
