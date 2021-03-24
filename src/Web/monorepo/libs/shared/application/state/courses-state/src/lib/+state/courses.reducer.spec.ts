import { CoursesEntity } from './courses.models';
import * as CoursesActions from './courses.actions';
import { State, initialState, reducer } from './courses.reducer';

describe('Courses Reducer', () => {
  const createCoursesEntity = (id: string, name = '') =>
    ({
      id,
      name: name || `name-${id}`,
    } as CoursesEntity);

  beforeEach(() => {});

  describe('valid Courses actions', () => {
    it('loadCoursesSuccess should return set the list of known Courses', () => {
      const courses = [
        createCoursesEntity('PRODUCT-AAA'),
        createCoursesEntity('PRODUCT-zzz'),
      ];
      const action = CoursesActions.loadCoursesSuccess({ courses });

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
