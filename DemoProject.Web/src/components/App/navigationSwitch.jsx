import React from 'react';
import { Switch, Route } from 'react-router-dom';
import { RoutePath } from '@/_helpers';
import { MainNavMenu } from '@/components/Main';
import { AccountNavMenu } from '@/components/Account';
import { AdminNavMenu } from '@/components/Admin';

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