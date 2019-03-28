import { Map } from 'immutable';
import { userConstants } from '../_constants';

const defaultState = Map({
  'loading': false,
  'users': null,
  'error': null
});

// (prevState, action) => newState
export function userReducer(state = defaultState, action) {
  switch (action.type) {
    case userConstants.GETALL_REQUEST:
      return state
        .set('loading', true)
        .set('users', null)
        .set('error', null);

    case userConstants.GETALL_SUCCESS:
      return state
        .set('loading', false)
        .set('users', action.users)
        .set('error', null);

    case userConstants.GETALL_FAILURE:
      return state
        .set('loading', false)
        .set('users', null)
        .set('error', action.error);

    default:
      return state;
  }
}