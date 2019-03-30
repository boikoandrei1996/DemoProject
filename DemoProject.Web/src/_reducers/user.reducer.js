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
      return defaultState.set('loading', true);

    case userConstants.GETALL_SUCCESS:
      return defaultState.set('users', action.users);

    case userConstants.GETALL_FAILURE:
      return defaultState.set('error', action.error);

    case userConstants.DELETE_REQUEST:
      return state
        .update(
          'users',
          items => items.map(item => item.get('id') === action.id ? item.set('deleting', true) : item)
        )
        .set('error', null);

    case userConstants.DELETE_SUCCESS:
      return state
        .update(
          'users',
          items => items.filter(item => item.get('id') !== action.id)
        );

    case userConstants.DELETE_FAILURE:
      return state
        .update(
          'users',
          items => items.map(item => item.get('id') === action.id ? item.set('deleting', false) : item)
        )
        .set('error', action.error);

    default:
      return state;
  }
}