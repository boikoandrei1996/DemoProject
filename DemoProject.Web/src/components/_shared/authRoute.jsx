import React from 'react';
import { Route, Redirect } from 'react-router-dom';
import { RoutePath } from '@/_constants';
import { userService } from '@/_services';

export const AuthRoute = ({ component: Component, roles, ...rest }) => (
  <Route {...rest} render={props => {
    const currentUser = userService.getCurrentUser();

    if (currentUser && currentUser.user && currentUser.user.role) {
      // check if route is restricted by role
      const currentUserRole = currentUser.user.role.toLowerCase();
      if (roles && Array.isArray(roles) && roles.indexOf(currentUserRole) === -1) {
        return (<Redirect exact to={RoutePath.LOGIN} />);
      }

      return (<Component {...props} />);
    }
    else {
      // not logged in so redirect to login page with the return url
      return (<Redirect exact to={RoutePath.LOGIN} />);
    }
  }} />
)