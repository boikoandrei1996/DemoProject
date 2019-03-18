import { combineReducers } from 'redux-immutable';
import { phoneReducer } from './phone.reducer';

export const rootReducer = combineReducers({
  phoneState: phoneReducer
});