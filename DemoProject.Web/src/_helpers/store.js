import { createStore, applyMiddleware } from "redux";
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

export const store = createStore(
  rootReducer,
  applyMiddleware(...middlewares)
);