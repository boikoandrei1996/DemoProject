import React from "react";
import ReactDOM from "react-dom";
import { createStore } from "redux";
import { Provider } from "react-redux";
import { ConnectedRouter } from 'react-router-redux';
import { createBrowserHistory } from 'history';
import reducer from "./reducers/reducer";
import * as actionTypes from './constants/actionTypeNames';
import Layout from "./components/layout.jsx";

var store = createStore(reducer);
store.dispatch({
  type: actionTypes.SET_INITIAL_STATE,
  defaultData: {
    phones: ["iPhone 7 Plus", "Samsung Galaxy A5", "qwe"]
  }
});

var history = createBrowserHistory({
  basename: document.getElementsByTagName('base')[0].getAttribute('href')
});

ReactDOM.render(
  <Provider store={store}>
    <ConnectedRouter history={history}>
      <Layout />
    </ConnectedRouter>
  </Provider>,
  document.getElementById("root")
);