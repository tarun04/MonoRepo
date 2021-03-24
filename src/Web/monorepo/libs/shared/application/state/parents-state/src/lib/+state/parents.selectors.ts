import { createFeatureSelector, createSelector } from '@ngrx/store';
import {
  PARENTS_FEATURE_KEY,
  State,
  ParentsPartialState,
  parentsAdapter,
} from './parents.reducer';

// Lookup the 'Parents' feature state managed by NgRx
export const getParentsState = createFeatureSelector<
  ParentsPartialState,
  State
>(PARENTS_FEATURE_KEY);

const { selectAll, selectEntities } = parentsAdapter.getSelectors();

export const getParentsLoaded = createSelector(
  getParentsState,
  (state: State) => state.loaded
);

export const getParentsError = createSelector(
  getParentsState,
  (state: State) => state.error
);

export const getAllParents = createSelector(getParentsState, (state: State) =>
  selectAll(state)
);

export const getParentsEntities = createSelector(
  getParentsState,
  (state: State) => selectEntities(state)
);

export const getSelectedId = createSelector(
  getParentsState,
  (state: State) => state.selectedId
);

export const getSelected = createSelector(
  getParentsEntities,
  getSelectedId,
  (entities, selectedId) => selectedId && entities[selectedId]
);
