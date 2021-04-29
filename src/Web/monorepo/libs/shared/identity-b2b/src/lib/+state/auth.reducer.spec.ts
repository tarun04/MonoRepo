import { AuthUser } from '@monorepo/shared/data-models';
import * as AuthActions from './auth.actions';
import { State, initialState, reducer } from './auth.reducer';

describe('Auth Reducer', () => {
  const createAuthEntity = (id: string, name = '') =>
    ({
      sub: id,
      given_name: name || `name-${id}`,
    } as AuthUser);

  beforeEach(() => {});

  describe('valid Auth actions', () => {
    it('loginSuccess should return set the list of known Auth', () => {
      const authUser = createAuthEntity('PRODUCT-AAA');
      const action = AuthActions.loginSuccess({ authUser });

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
