import React from "react";
import { render } from "react-dom";
import { Provider } from "react-redux";
import { ConnectedRouter } from 'react-router-redux';

import { store, history } from '@/_helpers';
import { phoneConstants } from '@/_constants';
import App from "@/components/app";
import 'bootstrap/dist/css/bootstrap.min.css';

store.dispatch({
  type: phoneConstants.SET_INITIAL_STATE,
  defaultData: {
    phones: ["iPhone 7 Plus", "Samsung Galaxy A5", "qwe"]
  }
});

render(
  <Provider store={store}>
    <ConnectedRouter history={history}>
      <App />
    </ConnectedRouter>
  </Provider>,
  document.getElementById("root")
);