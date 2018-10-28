import * as actionTypes from './actionTypeNames';

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