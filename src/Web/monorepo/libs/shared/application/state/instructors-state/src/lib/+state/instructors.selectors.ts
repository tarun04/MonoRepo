import { createFeatureSelector, createSelector } from '@ngrx/store';
import {
  INSTRUCTORS_FEATURE_KEY,
  State,
  InstructorsPartialState,
  instructorsAdapter,
} from './instructors.reducer';

// Lookup the 'Instructors' feature state managed by NgRx
export const getInstructorsState = createFeatureSelector<
  InstructorsPartialState,
  State
>(INSTRUCTORS_FEATURE_KEY);

const { selectAll, selectEntities } = instructorsAdapter.getSelectors();

export const getInstructorsLoaded = createSelector(
  getInstructorsState,
  (state: State) => state.loaded
);

export const getInstructorsError = createSelector(
  getInstructorsState,
  (state: State) => state.error
);

export const getAllInstructors = createSelector(
  getInstructorsState,
  (state: State) => selectAll(state)
);

export const getInstructorsEntities = createSelector(
  getInstructorsState,
  (state: State) => selectEntities(state)
);

export const getSelectedId = createSelector(
  getInstructorsState,
  (state: State) => state.selectedId
);

export const getSelected = createSelector(
  getInstructorsEntities,
  getSelectedId,
  (entities, selectedId) => selectedId && entities[selectedId]
);
