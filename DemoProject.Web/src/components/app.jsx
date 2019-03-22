import React from 'react';
import { Switch, Route } from 'react-router-dom';
import { Container, Row, Col } from 'react-bootstrap';

import { NavBarMenu, NavMenu } from './shared';
import { HomePage, DiscountPage, TempPage, NotFoundPage } from './pages';
import 'bootstrap/dist/css/bootstrap.min.css';
import '@/_styles/index.sass';

class App extends React.Component {
  render() {
    return (
      <Container fluid>
        <Row className='navbar-row'>
          <NavBarMenu />
        </Row>
        <Row>
          <Col md={2} className='navmenu-col'>
            <NavMenu />
          </Col>
          <Col md={10} className='content-col'>
            <Switch>
              <Route exact path='/' component={HomePage} />
              <Route exact path='/discount' component={DiscountPage} />
              <Route exact path='/temp/:id(\d+)' component={TempPage} />
              <Route component={NotFoundPage} />
            </Switch>
          </Col>
        </Row>
      </Container>
    );
  }
}

export default App;