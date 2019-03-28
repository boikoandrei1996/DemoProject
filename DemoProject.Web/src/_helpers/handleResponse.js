import { fromJS } from 'immutable';

export function handleResponse(response) {
  return response.text()
    .then(text => {
      const data = text && JSON.parse(text);

      if (!response.ok) {
        if ([401, 403].indexOf(response.status) !== -1) {
          // auto logout if 401 Unauthorized or 403 Forbidden response returned from api
          // authenticationService.logout();
          // location.reload(true);
          alert('Unauthorized or Forbidden response');
        }

        const errorMessage = (data && data.message) || response.statusText;
        return Promise.reject(errorMessage);
      }

      return fromJS(data);
    });
}