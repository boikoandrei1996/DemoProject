import { Map } from 'immutable';
import { phoneConstants } from '@/_constants';

export function phoneReducer(state = Map(), action) {
  switch (action.type) {
    case phoneConstants.SET_INITIAL_STATE:
      return state.merge(action.defaultData);

    case phoneConstants.ADD_PHONE:
      return state.update(
        'phones',
        (phones) => phones.push(action.phone)
      );

    case phoneConstants.DELETE_PHONE:
      return state.update(
        'phones',
        (phones) => phones.filterNot(
          (item) => item === action.phone
        )
      );

    default:
      return state;
  }
}