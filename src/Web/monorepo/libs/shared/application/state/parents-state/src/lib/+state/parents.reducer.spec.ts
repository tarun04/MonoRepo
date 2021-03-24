import { ParentsEntity } from './parents.models';
import * as ParentsActions from './parents.actions';
import { State, initialState, reducer } from './parents.reducer';

describe('Parents Reducer', () => {
  const createParentsEntity = (id: string, name = '') =>
    ({
      id,
      name: name || `name-${id}`,
    } as ParentsEntity);

  beforeEach(() => {});

  describe('valid Parents actions', () => {
    it('loadParentsSuccess should return set the list of known Parents', () => {
      const parents = [
        createParentsEntity('PRODUCT-AAA'),
        createParentsEntity('PRODUCT-zzz'),
      ];
      const action = ParentsActions.loadParentsSuccess({ parents });

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
