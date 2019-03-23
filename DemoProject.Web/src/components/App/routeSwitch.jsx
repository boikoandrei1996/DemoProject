import React from 'react';
import { Switch, Route } from 'react-router-dom';
import { HomePage, DiscountPage, TempPage, NotFoundPage } from '@/components/pages';

class RouteSwitch extends React.Component {
  render() {
    return (
      <Switch>
        <Route exact path='/' component={HomePage} />
        <Route exact path='/discount' component={DiscountPage} />
        <Route exact path='/temp/:id(\d+)' component={TempPage} />
        <Route component={NotFoundPage} />
      </Switch>
    );
  }
}

export { RouteSwitch };