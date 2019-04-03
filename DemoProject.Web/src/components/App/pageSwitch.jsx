import React from 'react';
import { Switch, Route } from 'react-router-dom';
import { RoutePath } from '@/_constants';
import { NotFoundPage } from '@/components/_shared';
import { MainHomePage } from '@/components/Main';
import { AccountHomePage } from '@/components/Account';
import { AdminHomePage, UsersPage, UsersPageAll, RegisterUserPage } from '@/components/Admin';

class PageSwitch extends React.Component {
  render() {
    return (
      <Switch>
        <Route exact path={RoutePath.MAIN_TEMPLATE} component={MainHomePage} />
        <Route exact path={RoutePath.ACCOUNT} component={AccountHomePage} />
        <Route exact path={RoutePath.ADMIN} component={AdminHomePage} />
        <Route exact path={RoutePath.ADMIN_USERS_ALL} component={UsersPageAll} />
        <Route exact path={RoutePath.ADMIN_USERS_PAGE_TEMPLATE} component={UsersPage} />
        <Route exact path={RoutePath.ADMIN_REGISTER_USER} component={RegisterUserPage} />
        <Route component={NotFoundPage} />
      </Switch>
    );
  }
}

export { PageSwitch };