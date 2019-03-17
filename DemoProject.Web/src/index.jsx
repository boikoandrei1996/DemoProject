import React from "react";
import { render } from "react-dom";
import { Provider } from "react-redux";
import { ConnectedRouter } from 'react-router-redux';
import { createBrowserHistory } from 'history';

import { createStore } from "redux";
// import { rootReducer } from "./_reducers";
import { phoneReducer } from "./_reducers/phone.reducer";

import { phoneConstants } from './_constants';
import Layout from "./components/layout";
import 'bootstrap/dist/css/bootstrap.min.css';

var store = createStore(phoneReducer);

store.dispatch({
  type: phoneConstants.SET_INITIAL_STATE,
  defaultData: {
    phones: ["iPhone 7 Plus", "Samsung Galaxy A5", "qwe"]
  }
});

var history = createBrowserHistory({
  // basename: document.getElementsByTagName('base')[0].getAttribute('href')
});

render(
  <Provider store={store}>
    <ConnectedRouter history={history}>
      <Layout />
    </ConnectedRouter>
  </Provider>,
  document.getElementById("root")
);