import { createAction, props } from '@ngrx/store';

import { AuthUser } from '@monorepo/shared/data-models';

export const login = createAction(
  '[Auth/API] Login',
  props<{ path: string }>()
);
export const loginInProgress = createAction('[Auth/API] Login In Progress');
export const loginSuccess = createAction(
  '[Auth/API] Login Success',
  props<{ authUser: AuthUser }>()
);
export const loginFailure = createAction(
  '[Auth/API] Login Failure',
  props<{ error: any }>()
);

export const logout = createAction('[Auth/API] Logout');
export const logoutFailure = createAction(
  '[Auth/API] Logout Failure',
  props<{ error: any }>()
);

export const loadAuthUser = createAction('[Auth/API] Login');
export const loadAuthUserSuccess = createAction(
  '[Auth/API] Login',
  props<{ authUser: AuthUser }>()
);
export const loadAuthUserFailure = createAction(
  '[Auth/API] Login',
  props<{ error: any }>()
);
