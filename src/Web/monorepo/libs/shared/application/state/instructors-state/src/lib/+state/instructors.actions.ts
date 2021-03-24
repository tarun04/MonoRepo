import { createAction, props } from '@ngrx/store';
import { InstructorsEntity } from './instructors.models';

export const init = createAction('[Instructors Page] Init');

export const loadInstructorsSuccess = createAction(
  '[Instructors/API] Load Instructors Success',
  props<{ instructors: InstructorsEntity[] }>()
);

export const loadInstructorsFailure = createAction(
  '[Instructors/API] Load Instructors Failure',
  props<{ error: any }>()
);
