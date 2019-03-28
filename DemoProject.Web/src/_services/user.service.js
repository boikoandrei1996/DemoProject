import config from 'config';
import { handleResponse } from '../_helpers';

export const userService = {
  getAll,
  getById
};

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