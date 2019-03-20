import React from "react";
import { render } from "react-dom";
import { Provider } from "react-redux";
import { ConnectedRouter } from 'react-router-redux';

import { store, history } from '@/_helpers';
import App from "@/components/app";
import 'bootstrap/dist/css/bootstrap.min.css';

render(
  <Provider store={store}>
    <ConnectedRouter history={history}>
      <App />
    </ConnectedRouter>
  </Provider>,
  document.getElementById("root")
);