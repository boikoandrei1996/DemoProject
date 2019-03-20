import React from 'react';
import { Switch, Route } from 'react-router-dom';
import { Grid, Col, Row } from 'react-bootstrap';

import { NavMenu } from './shared';
import { HomePage, DiscountPage, TempPage, NotFoundPage } from './pages';
import '@/_styles/index.sass';

class App extends React.Component {
  render() {
    return (
      <Grid>
        <Row>
          <NavMenu />
        </Row>
        <Row className='main'>
          <Switch>
            <Route exact path='/' component={HomePage} />
            <Route exact path='/discount' component={DiscountPage} />
            <Route exact path='/temp/:id(\d+)' component={TempPage} />
            <Route component={NotFoundPage} />
          </Switch>
        </Row>
      </Grid>
    );
  }
}

export default App;