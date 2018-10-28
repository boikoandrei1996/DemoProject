import React from 'react';
import { Route, Switch } from 'react-router-dom';
import { Col, Grid, Row } from 'react-bootstrap';
import NavMenu from './navMenu.jsx';
import PhoneView from './phoneView.jsx'
import PhoneView2 from './phoneView2.jsx'
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
              <Route path='/phone/:id(\d+)' component={PhoneView2} />
              <Route component={NotFound} />
            </Switch>
          </Col>
        </Row>
      </Grid>
    );
  }
}

export default Layout;