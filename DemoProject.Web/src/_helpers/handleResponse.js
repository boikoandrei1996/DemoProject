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

        let errors;
        if (data && Array.isArray(data) && data.length > 0) {
          errors = data.map(x => x.description.toString());
        }
        else if (data && data.message) {
          errors = [data.message];
        }
        else {
          errors = [response.statusText];
        }

        if (errors.length > 3) {
          errors = errors.slice(0, 3)
        }

        return Promise.reject(fromJS(errors));
      }

      return fromJS(data);
    });
}