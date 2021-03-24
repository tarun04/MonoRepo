import { createAction, props } from '@ngrx/store';
import { StudentsEntity } from './students.models';

export const init = createAction('[Students Page] Init');

export const loadStudentsSuccess = createAction(
  '[Students/API] Load Students Success',
  props<{ students: StudentsEntity[] }>()
);

export const loadStudentsFailure = createAction(
  '[Students/API] Load Students Failure',
  props<{ error: any }>()
);
