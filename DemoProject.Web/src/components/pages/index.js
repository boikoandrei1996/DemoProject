import React from 'react';
import { Route, Switch } from 'react-router-dom';
import { HomePage, DiscountPage, TempPage, NotFoundPage } from '.';

// should be firstly const, then export!
const Routes = (
  <Switch>
    <Route exact path='/' component={HomePage} />
    <Route exact path='/discount' component={DiscountPage} />
    <Route path='/temp/:id(\d+)' component={TempPage} />
    <Route component={NotFoundPage} />
  </Switch>
);

export { Routes };
export * from './discountPage';
export * from './notFoundPage';
export * from './homePage';
export * from './tempPage';