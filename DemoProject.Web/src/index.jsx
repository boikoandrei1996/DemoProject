import React from "react";
import { render } from "react-dom";
import { Provider } from "react-redux";
import { ConnectedRouter } from 'react-router-redux';

import { history, store } from '@/_helpers';
import { phoneConstants } from '@/_constants';
import Layout from "@/components/layout";
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
      <Layout />
    </ConnectedRouter>
  </Provider>,
  document.getElementById("root")
);