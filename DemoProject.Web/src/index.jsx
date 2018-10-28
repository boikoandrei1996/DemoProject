import React from "react";
import ReactDOM from "react-dom";
import { createStore } from "redux";
import { Provider } from "react-redux";
import { ConnectedRouter } from 'react-router-redux';
import { createBrowserHistory } from 'history';
import reducer from "./phone/reducer.jsx";
import Layout from "./shared/layout.jsx";

var store = createStore(reducer);
store.dispatch({
  type: "DEFAULT_STATE",
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