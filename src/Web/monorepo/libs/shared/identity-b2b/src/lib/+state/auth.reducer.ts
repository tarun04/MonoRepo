import { createReducer, on, Action } from '@ngrx/store';
import { EntityState, EntityAdapter, createEntityAdapter } from '@ngrx/entity';

import { AuthUser } from '@monorepo/shared/data-models';
import * as fromActions from './auth.actions';

export const AUTH_FEATURE_KEY = 'auth';

export interface State extends EntityState<AuthUser> {
  selectedId?: string | number; // which Auth record has been selected
  loaded: boolean; // has the Auth list been loaded
  error?: string | null; // last known error (if any)
  authenticated: boolean; // has the user been authenticated
  user: AuthUser;
}

export interface AuthPartialState {
  readonly [AUTH_FEATURE_KEY]: State;
}

export const authAdapter: EntityAdapter<AuthUser> = createEntityAdapter<AuthUser>();

export const initialState: State = authAdapter.getInitialState({
  // set initial required properties
  loaded: false,
  authenticated: false,
  user: undefined,
});

const authReducer = createReducer(
  initialState,
  on(fromActions.login, (state) => ({ ...state, loaded: false, error: null })),
  on(fromActions.loginInProgress, (state) => ({
    ...state,
    loaded: false,
    authenticated: false,
    error: null,
  })),
  on(fromActions.loginSuccess, (state, { authUser }) =>
    authAdapter.setOne(authUser, {
      ...state,
      loaded: true,
      authenticated: true,
      error: null,
    })
  ),
  on(fromActions.loginFailure, (state, { error }) => ({ ...state, error })),

  on(fromActions.logout, (state) => ({
    ...state,
    loaded: false,
    error: null,
    authenticated: false,
    user: undefined,
  })),
  on(fromActions.logoutFailure, (state, { error }) => ({ ...state, error })),

  on(fromActions.loadAuthUser, (state) => ({
    ...state,
    loaded: false,
    error: null,
  })),
  on(fromActions.loadAuthUserSuccess, (state, { authUser }) =>
    authAdapter.setOne(authUser, {
      ...state,
      loaded: true,
      authenticated: true,
      error: null,
    })
  ),
  on(fromActions.loadAuthUserFailure, (state, { error }) => ({
    ...state,
    error,
  }))
);

export function reducer(state: State | undefined, action: Action) {
  return authReducer(state, action);
}
