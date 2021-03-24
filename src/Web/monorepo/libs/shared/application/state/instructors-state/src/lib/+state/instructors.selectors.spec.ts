import { InstructorsEntity } from './instructors.models';
import { State, instructorsAdapter, initialState } from './instructors.reducer';
import * as InstructorsSelectors from './instructors.selectors';

describe('Instructors Selectors', () => {
  const ERROR_MSG = 'No Error Available';
  const getInstructorsId = (it) => it['id'];
  const createInstructorsEntity = (id: string, name = '') =>
    ({
      id,
      name: name || `name-${id}`,
    } as InstructorsEntity);

  let state;

  beforeEach(() => {
    state = {
      instructors: instructorsAdapter.setAll(
        [
          createInstructorsEntity('PRODUCT-AAA'),
          createInstructorsEntity('PRODUCT-BBB'),
          createInstructorsEntity('PRODUCT-CCC'),
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

  describe('Instructors Selectors', () => {
    it('getAllInstructors() should return the list of Instructors', () => {
      const results = InstructorsSelectors.getAllInstructors(state);
      const selId = getInstructorsId(results[1]);

      expect(results.length).toBe(3);
      expect(selId).toBe('PRODUCT-BBB');
    });

    it('getSelected() should return the selected Entity', () => {
      const result = InstructorsSelectors.getSelected(state);
      const selId = getInstructorsId(result);

      expect(selId).toBe('PRODUCT-BBB');
    });

    it("getInstructorsLoaded() should return the current 'loaded' status", () => {
      const result = InstructorsSelectors.getInstructorsLoaded(state);

      expect(result).toBe(true);
    });

    it("getInstructorsError() should return the current 'error' state", () => {
      const result = InstructorsSelectors.getInstructorsError(state);

      expect(result).toBe(ERROR_MSG);
    });
  });
});
