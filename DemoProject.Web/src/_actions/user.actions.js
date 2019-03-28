import { userConstants } from '@/_constants';
import { userService } from '@/_services';

export const userActions = {
  getAll,
  remove
};

function getAll() {
  return (dispatch, getState) => {
    // let state = getState();

    dispatch(request());

    userService.getAll()
      .then(
        users => dispatch(success(users)),
        error => dispatch(failure(error.toString()))
      );
  };

  function request() { return { type: userConstants.GETALL_REQUEST }; }
  function success(users) { return { type: userConstants.GETALL_SUCCESS, users }; }
  function failure(error) { return { type: userConstants.GETALL_FAILURE, error }; }
}

function remove(id) {
  return dispatch => {
    dispatch(request(id));

    userService.remove(id)
      .then(
        () => dispatch(success(id)),
        error => dispatch(failure(id, error.toString()))
      );
  };

  function request(id) { return { type: userConstants.DELETE_REQUEST, id }; }
  function success(id) { return { type: userConstants.DELETE_SUCCESS, id }; }
  function failure(id, error) { return { type: userConstants.DELETE_FAILURE, id, error }; }
}