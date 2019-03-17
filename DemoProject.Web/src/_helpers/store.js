import { createStore } from "redux";
// import { rootReducer } from "@/_reducers";
import { phoneReducer } from "@/_reducers/phone.reducer";

export const store = createStore(phoneReducer);