import React from 'react';
import { Route, Switch } from 'react-router-dom';
import { Col, Grid, Row } from 'react-bootstrap';
import NavMenu from './navMenu.jsx';
import PhoneView from './phoneView.jsx'
import TempView from './tempView.jsx'
import NotFound from './notFound.jsx'

class Layout extends React.Component {
  render() {
    return (
      <Grid fluid>
        <Row>
          <Col sm={3}>
            <NavMenu />
          </Col>
          <Col sm={9}>
            <Switch>
              <Route exact path='/' component={PhoneView} />
              <Route path='/temp/:id(\d+)' component={TempView} />
              <Route component={NotFound} />
            </Switch>
          </Col>
        </Row>
      </Grid>
    );
  }
}

export default Layout;