import { createStore, applyMiddleware } from "redux";
import { fromJS } from 'immutable';
import thunk from 'redux-thunk';
import { createLogger } from 'redux-logger';
import { rootReducer } from "@/_reducers";

const middlewares = [
  thunk
];

const isDevelopment = process.env.NODE_ENV === 'development';
if (isDevelopment) {
  const logger = createLogger({
    // ...options
  });
  middlewares.push(logger);
}

const initialState = {
  phoneState: {
    phones: ["iPhone 7 Plus", "Samsung Galaxy A5", "qwe"]
  }
};

export const store = createStore(
  rootReducer,
  fromJS(initialState),
  applyMiddleware(...middlewares)
);