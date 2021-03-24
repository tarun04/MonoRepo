import { createAction, props } from '@ngrx/store';
import { ParentsEntity } from './parents.models';

export const init = createAction('[Parents Page] Init');

export const loadParentsSuccess = createAction(
  '[Parents/API] Load Parents Success',
  props<{ parents: ParentsEntity[] }>()
);

export const loadParentsFailure = createAction(
  '[Parents/API] Load Parents Failure',
  props<{ error: any }>()
);
