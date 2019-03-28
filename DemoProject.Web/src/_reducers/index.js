import { combineReducers } from 'redux-immutable';
import { phoneReducer } from './phone.reducer';
import { userReducer } from './user.reducer';

export const rootReducer = combineReducers({
  phoneState: phoneReducer,
  userState: userReducer
});