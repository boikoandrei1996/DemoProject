import * as actionTypes from '../constants/actionTypeNames';

var addPhone = function (phone) {
  return {
    type: actionTypes.ADD_PHONE,
    phone
  }
};

var deletePhone = function (phone) {
  return {
    type: actionTypes.DELETE_PHONE,
    phone
  }
};

export default {
  addPhone,
  deletePhone
};