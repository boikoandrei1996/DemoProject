import { Map } from "immutable";

var reducer = function (state = Map(), action) {
  switch (action.type) {
    case "DEFAULT_STATE":
      return state.merge(action.defaultData);

    case "ADD_PHONE":
      return state.update(
        "phones",
        (phones) => phones.push(action.phone)
      );

    case "DELETE_PHONE":
      return state.update(
        "phones",
        (phones) => phones.filterNot(
          (item) => item === action.phone
        )
      );
  }

  return state;
}

export default reducer;