import React from "react";
import ReactDOM from "react-dom";
import { createStore } from "redux";
import { Provider } from "react-redux";
import reducer from "./app/reducer.jsx";
import AppView from "./app/appView.jsx";

var store = createStore(reducer);
store.dispatch({
  type: "DEFAULT_STATE",
  defaultData: {
    phones: ["iPhone 7 Plus", "Samsung Galaxy A5", "qwe"]
  }
});

ReactDOM.render(
  <Provider store={store}>
    <AppView />
  </Provider>,
  document.getElementById("root")
);