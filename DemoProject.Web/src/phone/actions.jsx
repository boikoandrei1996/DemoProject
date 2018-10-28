var addPhone = function (phone) {
  return {
    type: "ADD_PHONE",
    phone
  }
};

var deletePhone = function (phone) {
  return {
    type: "DELETE_PHONE",
    phone
  }
};

export default {
  addPhone,
  deletePhone
};