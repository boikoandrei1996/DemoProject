import React from 'react';
import { Route, Switch } from 'react-router-dom';
import PhoneView from './pages/phoneView.jsx';
import TempView from './pages/tempView.jsx';
import NotFoundView from './pages/notFoundView.jsx';

export default Routes = (
  <Switch>
    <Route exact path='/' component={PhoneView} />
    <Route path='/temp/:id(\d+)' component={TempView} />
    <Route component={NotFoundView} />
  </Switch>
);