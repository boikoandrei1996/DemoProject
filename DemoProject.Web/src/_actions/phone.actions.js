import { phoneConstants } from '../_constants';

function addPhone(phone) {
  return {
    type: phoneConstants.ADD_PHONE,
    phone
  };
}

function deletePhone(phone) {
  return {
    type: phoneConstants.DELETE_PHONE,
    phone
  };
}

export const phoneActions = {
  addPhone,
  deletePhone
};