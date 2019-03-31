import config from 'config';
import { handleResponse } from '@/_helpers';

export const userService = {
  getPage,
  getAll,
  getById,
  remove,
  register
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