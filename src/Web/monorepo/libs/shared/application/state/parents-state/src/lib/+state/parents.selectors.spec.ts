import { ParentsEntity } from './parents.models';
import { State, parentsAdapter, initialState } from './parents.reducer';
import * as ParentsSelectors from './parents.selectors';

describe('Parents Selectors', () => {
  const ERROR_MSG = 'No Error Available';
  const getParentsId = (it) => it['id'];
  const createParentsEntity = (id: string, name = '') =>
    ({
      id,
      name: name || `name-${id}`,
    } as ParentsEntity);

  let state;

  beforeEach(() => {
    state = {
      parents: parentsAdapter.setAll(
        [
          createParentsEntity('PRODUCT-AAA'),
          createParentsEntity('PRODUCT-BBB'),
          createParentsEntity('PRODUCT-CCC'),
        ],
        {
          ...initialState,
          selectedId: 'PRODUCT-BBB',
          error: ERROR_MSG,
          loaded: true,
        }
      ),
    };
  });

  describe('Parents Selectors', () => {
    it('getAllParents() should return the list of Parents', () => {
      const results = ParentsSelectors.getAllParents(state);
      const selId = getParentsId(results[1]);

      expect(results.length).toBe(3);
      expect(selId).toBe('PRODUCT-BBB');
    });

    it('getSelected() should return the selected Entity', () => {
      const result = ParentsSelectors.getSelected(state);
      const selId = getParentsId(result);

      expect(selId).toBe('PRODUCT-BBB');
    });

    it("getParentsLoaded() should return the current 'loaded' status", () => {
      const result = ParentsSelectors.getParentsLoaded(state);

      expect(result).toBe(true);
    });

    it("getParentsError() should return the current 'error' state", () => {
      const result = ParentsSelectors.getParentsError(state);

      expect(result).toBe(ERROR_MSG);
    });
  });
});
