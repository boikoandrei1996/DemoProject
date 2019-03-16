import React from "react";
import { render } from "react-dom";
import { createStore } from "redux";
import { Provider } from "react-redux";
import { ConnectedRouter } from 'react-router-redux';
import { createBrowserHistory } from 'history';
import phoneReducer from "./reducers/phoneReducer";
import * as actionTypes from './constants/phoneActionTypeNames';
import Layout from "./components/layout.jsx";
import 'bootstrap/dist/css/bootstrap.min.css';

var store = createStore(phoneReducer);
store.dispatch({
  type: actionTypes.SET_INITIAL_STATE,
  defaultData: {
    phones: ["iPhone 7 Plus", "Samsung Galaxy A5", "qwe"]
  }
});

var history = createBrowserHistory({
  basename: document.getElementsByTagName('base')[0].getAttribute('href')
});

render(
  <Provider store={store}>
    <ConnectedRouter history={history}>
      <Layout />
    </ConnectedRouter>
  </Provider>,
  document.getElementById("root")
);