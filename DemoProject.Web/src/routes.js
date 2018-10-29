import React from 'react';
import { Route, Switch } from 'react-router-dom';
import PhoneView from './components/phoneView.jsx';
import TempView from './components/tempView.jsx';
import NotFound from './components/notFound.jsx';

const Routes = (
  <Switch>
    <Route exact path='/' component={PhoneView} />
    <Route path='/temp/:id(\d+)' component={TempView} />
    <Route component={NotFound} />
  </Switch>
);

export default Routes;