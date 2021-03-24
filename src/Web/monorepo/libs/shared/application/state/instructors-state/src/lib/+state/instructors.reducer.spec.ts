import { InstructorsEntity } from './instructors.models';
import * as InstructorsActions from './instructors.actions';
import { State, initialState, reducer } from './instructors.reducer';

describe('Instructors Reducer', () => {
  const createInstructorsEntity = (id: string, name = '') =>
    ({
      id,
      name: name || `name-${id}`,
    } as InstructorsEntity);

  beforeEach(() => {});

  describe('valid Instructors actions', () => {
    it('loadInstructorsSuccess should return set the list of known Instructors', () => {
      const instructors = [
        createInstructorsEntity('PRODUCT-AAA'),
        createInstructorsEntity('PRODUCT-zzz'),
      ];
      const action = InstructorsActions.loadInstructorsSuccess({ instructors });

      const result: State = reducer(initialState, action);

      expect(result.loaded).toBe(true);
      expect(result.ids.length).toBe(2);
    });
  });

  describe('unknown action', () => {
    it('should return the previous state', () => {
      const action = {} as any;

      const result = reducer(initialState, action);

      expect(result).toBe(initialState);
    });
  });
});
