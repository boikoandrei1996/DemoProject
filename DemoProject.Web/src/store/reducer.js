import { Map } from 'immutable';
import * as actionTypes from './actionTypeNames';

var reducer = function (state = Map(), action) {
  switch (action.type) {
    case actionTypes.SET_INITIAL_STATE:
      return state.merge(action.defaultData);

    case actionTypes.ADD_PHONE:
      return state.update(
        'phones',
        (phones) => phones.push(action.phone)
      );

    case actionTypes.DELETE_PHONE:
      return state.update(
        'phones',
        (phones) => phones.filterNot(
          (item) => item === action.phone
        )
      );
  }

  return state;
}

export default reducer;