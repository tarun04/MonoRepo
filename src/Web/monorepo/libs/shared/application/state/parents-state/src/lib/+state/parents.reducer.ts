import { createReducer, on, Action } from '@ngrx/store';
import { EntityState, EntityAdapter, createEntityAdapter } from '@ngrx/entity';

import * as ParentsActions from './parents.actions';
import { ParentsEntity } from './parents.models';

export const PARENTS_FEATURE_KEY = 'parents';

export interface State extends EntityState<ParentsEntity> {
  selectedId?: string | number; // which Parents record has been selected
  loaded: boolean; // has the Parents list been loaded
  error?: string | null; // last known error (if any)
}

export interface ParentsPartialState {
  readonly [PARENTS_FEATURE_KEY]: State;
}

export const parentsAdapter: EntityAdapter<ParentsEntity> = createEntityAdapter<ParentsEntity>();

export const initialState: State = parentsAdapter.getInitialState({
  // set initial required properties
  loaded: false,
});

const parentsReducer = createReducer(
  initialState,
  on(ParentsActions.init, (state) => ({
    ...state,
    loaded: false,
    error: null,
  })),
  on(ParentsActions.loadParentsSuccess, (state, { parents }) =>
    parentsAdapter.setAll(parents, { ...state, loaded: true })
  ),
  on(ParentsActions.loadParentsFailure, (state, { error }) => ({
    ...state,
    error,
  }))
);

export function reducer(state: State | undefined, action: Action) {
  return parentsReducer(state, action);
}
