import { phoneConstants } from '@/_constants';

export const phoneActions = {
  addPhone,
  deletePhone
};

function addPhone(phone) {
  return {
    type: phoneConstants.ADD_PHONE,
    phone
  };
}

function deletePhone(phone) {
  return (dispatch, getState) => {
    // let state = getState();

    dispatch({
      type: phoneConstants.DELETE_PHONE,
      phone
    });
  };
}