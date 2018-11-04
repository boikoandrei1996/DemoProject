import React from 'react';
import { Route, Switch } from 'react-router-dom';
import NotFoundPage from './pages/notFoundPage.jsx';
import PhoneView from './pages/phoneView.jsx';
import TempView from './pages/tempView.jsx';

// should be firstly const, then export!
const Routes = (
  <Switch>
    <Route exact path='/' component={PhoneView} />
    <Route path='/temp/:id(\d+)' component={TempView} />
    <Route component={NotFoundPage} />
  </Switch>
);

export default Routes;