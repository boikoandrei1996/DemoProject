import { toast } from 'react-toastify';
import { userConstants } from '@/_constants';
import { userService } from '@/_services';

export const userActions = {
  getPage,
  getAll,
  removeUser,
  registerUser,
  resetRegisterUserForm,
  loginUser,
  resetLoginForm,
  logoutUser
};

function getPage(index) {
  return (dispatch, getState) => {
    // let state = getState();
    dispatch(request());

    userService.getPage(index)
      .then(
        page => dispatch(success(page)),
        errors => dispatch(failure(errors))
      );
  };

  function request() { return { type: userConstants.GETPAGE_REQUEST }; }
  function success(page) { return { type: userConstants.GETPAGE_SUCCESS, page }; }
  function failure(errors) { return { type: userConstants.GETPAGE_FAILURE, errors }; }
}

function getAll() {
  return dispatch => {
    dispatch(request());

    userService.getAll()
      .then(
        users => dispatch(success(users)),
        errors => dispatch(failure(errors))
      );
  };

  function request() { return { type: userConstants.GETALL_REQUEST }; }
  function success(users) { return { type: userConstants.GETALL_SUCCESS, users }; }
  function failure(errors) { return { type: userConstants.GETALL_FAILURE, errors }; }
}

function removeUser(id) {
  return dispatch => {
    dispatch(request(id));

    userService.remove(id)
      .then(
        () => dispatch(success(id)),
        errors => dispatch(failure(id, errors))
      );
  };

  function request(id) { return { type: userConstants.DELETE_REQUEST, id }; }
  function success(id) { return { type: userConstants.DELETE_SUCCESS, id }; }
  function failure(id, errors) { return { type: userConstants.DELETE_FAILURE, id, errors }; }
}

function registerUser(user) {
  const toastSuccessMessage = `User ${user && user.get('username')} successfully registered!`;
  const toastErrorMessage = `User ${user && user.get('username')} registration is failed!`;

  return dispatch => {
    dispatch(request());

    userService.register(user)
      .then(
        () => {
          dispatch(success());
          toast.success(toastSuccessMessage);
          //history.push('/login');
        },
        errors => {
          dispatch(failure(errors));
          toast.error(toastErrorMessage);
        }
      );
  };

  function request() { return { type: userConstants.REGISTER_REQUEST }; }
  function success() { return { type: userConstants.REGISTER_SUCCESS }; }
  function failure(errors) { return { type: userConstants.REGISTER_FAILURE, errors }; }
}

function resetRegisterUserForm() {
  return { type: userConstants.REGISTER_FORM_RESET };
}

function loginUser(username, password) {
  const toastErrorMessage = `Login is failed!`;
  const loginFailMessage = 'Username or password are wrong!';

  return dispatch => {
    dispatch(request());

    userService.login(username, password)
      .then(
        user => {
          if (user) {
            userService.setCurrentUser(user);
            dispatch(success());
            // history.push('/');
          }
          else {
            dispatch(failure([loginFailMessage]));
            toast.error(toastErrorMessage);
          }
        },
        errors => {
          dispatch(failure(errors));
          toast.error(toastErrorMessage);
        }
      );
  };

  function request() { return { type: userConstants.LOGIN_REQUEST }; }
  function success() { return { type: userConstants.LOGIN_SUCCESS }; }
  function failure(errors) { return { type: userConstants.LOGIN_FAILURE, errors }; }
}

function resetLoginForm() {
  return { type: userConstants.LOGIN_FORM_RESET };
}

function logoutUser() {
  userService.logout();

  return { type: userConstants.LOGOUT };
}