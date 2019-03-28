import React from 'react';
import { Switch, Route } from 'react-router-dom';
import { RoutePath } from '@/_helpers';
import { NotFoundPage } from '@/components/_shared';
import { MainHomePage } from '@/components/Main';
import { AccountHomePage } from '@/components/Account';
import { AdminHomePage, UsersPage } from '@/components/Admin';

class PageSwitch extends React.Component {
  render() {
    return (
      <Switch>
        <Route exact path={RoutePath.MAIN_WITH_OPTIONAL_ID} component={MainHomePage} />
        <Route exact path={RoutePath.ACCOUNT} component={AccountHomePage} />
        <Route exact path={RoutePath.ADMIN} component={AdminHomePage} />
        <Route exact path={RoutePath.ADMIN_USERS} component={UsersPage} />
        <Route component={NotFoundPage} />
      </Switch>
    );
  }
}

export { PageSwitch };