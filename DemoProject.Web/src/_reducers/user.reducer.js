import { Map } from 'immutable';
import { userConstants } from '../_constants';

const defaultState = Map({
  'loading': false,
  'users': null,
  'errors': null,
  'registering': false
});

// (prevState, action) => newState
export function userReducer(state = defaultState, action) {
  switch (action.type) {
    case userConstants.GETALL_REQUEST:
      return defaultState.set('loading', true);

    case userConstants.GETALL_FAILURE:
      return defaultState.set('errors', action.errors);

    case userConstants.GETALL_SUCCESS:
      return defaultState.set('users', action.users);

    case userConstants.DELETE_REQUEST:
      return state
        .update(
          'users',
          items => items.map(item => item.get('id') === action.id ? item.set('deleting', true) : item)
        )
        .set('errors', null);

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
        .set('errors', action.errors);

    case userConstants.GETPAGE_REQUEST:
      return defaultState.set('loading', true);

    case userConstants.GETPAGE_FAILURE:
      return defaultState.set('errors', action.errors);

    case userConstants.GETPAGE_SUCCESS:
      return defaultState
        .set('totalPages', action.page.get('totalPages'))
        .set('users', action.page.get('records'));

    case userConstants.REGISTER_REQUEST:
      return state
        .set('registering', true)
        .set('errors', null);

    case userConstants.REGISTER_SUCCESS:
      return state.set('registering', false);

    case userConstants.REGISTER_FAILURE:
      return state
        .set('registering', false)
        .set('errors', action.errors);

    case userConstants.REGISTER_FORM_RESET:
      return state.set('errors', null);

    default:
      return state;
  }
}