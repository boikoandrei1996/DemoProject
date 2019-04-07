import config from 'config';
import { handleResponse } from '@/_helpers';

export const userService = {
  getPage,
  getAll,
  getById,
  remove,
  register,
  login,
  logout,
  getCurrentUser,
  setCurrentUser
};

function getPage(index) {
  const request = {
    method: 'GET',
    // headers: authHeader()
  };
  return fetch(`${config.apiUrl}/user/page/${index}`, request).then(handleResponse);
}

function getAll() {
  const request = {
    method: 'GET',
    // headers: authHeader()
  };
  return fetch(`${config.apiUrl}/user/all`, request).then(handleResponse);
}

function getById(id) {
  const request = {
    method: 'GET',
    // headers: authHeader()
  };
  return fetch(`${config.apiUrl}/user/${id}`, request).then(handleResponse);
}

function remove(id) {
  const request = {
    method: 'DELETE',
    // headers: authHeader()
  };

  return fetch(`${config.apiUrl}/user/${id}`, request).then(handleResponse);
}

function register(user) {
  const request = {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
      /*...authHeader()*/
    },
    body: JSON.stringify(user)
  };

  return fetch(`${config.apiUrl}/user`, request).then(handleResponse);
}

function login(username, password) {
  const request = {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify({ username, password })
  };

  return fetch(`${config.apiUrl}/user/authenticate`, request).then(handleResponse);
}

function logout() {
  localStorage.removeItem('user');
}

function getCurrentUser() {
  return JSON.parse(localStorage.getItem('user'));
}

function setCurrentUser(user) {
  localStorage.setItem('user', JSON.stringify(user));
}