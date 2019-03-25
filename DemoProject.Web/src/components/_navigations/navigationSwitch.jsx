import React from 'react';
import { Switch, Route } from 'react-router-dom';
import { MainNavMenu, AccountNavMenu, AdminNavMenu } from '.';
import { RoutePath } from '@/_helpers';

class NavigationSwitch extends React.Component {
  render() {
    return (
      <Switch>
        <Route path={RoutePath.MAIN} component={MainNavMenu} />
        <Route path={RoutePath.ACCOUNT} component={AccountNavMenu} />
        <Route path={RoutePath.ADMIN} component={AdminNavMenu} />
      </Switch>
    );
  }
}

export { NavigationSwitch };