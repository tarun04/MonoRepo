import { createReducer, on, Action } from '@ngrx/store';
import { EntityState, EntityAdapter, createEntityAdapter } from '@ngrx/entity';

import * as InstructorsActions from './instructors.actions';
import { InstructorsEntity } from './instructors.models';

export const INSTRUCTORS_FEATURE_KEY = 'instructors';

export interface State extends EntityState<InstructorsEntity> {
  selectedId?: string | number; // which Instructors record has been selected
  loaded: boolean; // has the Instructors list been loaded
  error?: string | null; // last known error (if any)
}

export interface InstructorsPartialState {
  readonly [INSTRUCTORS_FEATURE_KEY]: State;
}

export const instructorsAdapter: EntityAdapter<InstructorsEntity> = createEntityAdapter<InstructorsEntity>();

export const initialState: State = instructorsAdapter.getInitialState({
  // set initial required properties
  loaded: false,
});

const instructorsReducer = createReducer(
  initialState,
  on(InstructorsActions.init, (state) => ({
    ...state,
    loaded: false,
    error: null,
  })),
  on(InstructorsActions.loadInstructorsSuccess, (state, { instructors }) =>
    instructorsAdapter.setAll(instructors, { ...state, loaded: true })
  ),
  on(InstructorsActions.loadInstructorsFailure, (state, { error }) => ({
    ...state,
    error,
  }))
);

export function reducer(state: State | undefined, action: Action) {
  return instructorsReducer(state, action);
}
