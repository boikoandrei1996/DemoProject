import React from 'react';
import { Route, Switch } from 'react-router-dom';
import { Col, Grid, Row } from 'react-bootstrap';
import { NavMenu } from './shared';
import { HomePage, DiscountPage, TempPage, NotFoundPage } from './pages';

class App extends React.Component {
  render() {
    return (
      <Grid fluid>
        <Row>
          <Col sm={3}>
            <NavMenu />
          </Col>
          <Col sm={9}>
            <Switch>
              <Route exact path='/' component={HomePage} />
              <Route exact path='/discount' component={DiscountPage} />
              <Route exact path='/temp/:id(\d+)' component={TempPage} />
              <Route component={NotFoundPage} />
            </Switch>
          </Col>
        </Row>
      </Grid>
    );
  }
}

export default App;