import React from 'react';
import { Switch, Route } from 'react-router-dom';
import { RoutePath } from '@/_constants';
import { RoleAccessesEnum } from '@/_helpers';
import { NotFoundPage, AuthRoute } from '@/components/_shared';
import { MainHomePage } from '@/components/Main';
import { AccountHomePage, LoginPage, LogoutPage } from '@/components/Account';
import { AdminHomePage, UsersPage, RegisterUserPage } from '@/components/Admin';

class PageSwitch extends React.Component {
  render() {
    return (
      <Switch>
        <Route exact path={RoutePath.LOGIN} component={LoginPage} />
        <AuthRoute exact path={RoutePath.LOGOUT} component={LogoutPage} />

        <Route exact path={RoutePath.MAIN_TEMPLATE} component={MainHomePage} />
        <AuthRoute exact path={RoutePath.ACCOUNT} component={AccountHomePage} />
        <AuthRoute exact path={RoutePath.ADMIN} roles={RoleAccessesEnum.Admin} component={AdminHomePage} />
        <AuthRoute exact path={RoutePath.ADMIN_USERS_ALL} roles={RoleAccessesEnum.Admin} component={UsersPage} />
        <AuthRoute exact path={RoutePath.ADMIN_REGISTER_USER} roles={RoleAccessesEnum.Admin} component={RegisterUserPage} />
        {/*<Route exact path={RoutePath.ADMIN_USERS_PAGE_TEMPLATE} component={null} />*/}

        <Route component={NotFoundPage} />
      </Switch>
    );
  }
}

export { PageSwitch };