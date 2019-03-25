import React from 'react';
import { Switch, Route } from 'react-router-dom';
import { MainPage, AccountPage, AdminPage, NotFoundPage } from '.';
import { RoutePath } from '@/_helpers';

class PageSwitch extends React.Component {
  render() {
    return (
      <Switch>
        <Route exact path={RoutePath.MAIN_WITH_OPTIONAL_ID} component={MainPage} />
        <Route exact path={RoutePath.ACCOUNT} component={AccountPage} />
        <Route exact path={RoutePath.ADMIN} component={AdminPage} />
        <Route component={NotFoundPage} />
      </Switch>
    );
  }
}

export { PageSwitch };