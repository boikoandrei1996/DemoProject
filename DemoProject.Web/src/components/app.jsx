import React from 'react';
import { Container, Row, Col } from 'react-bootstrap';
import { NavBarMenu, NavMenu, RouteSwitch } from './shared';
import 'bootstrap/dist/css/bootstrap.min.css';
import '@/_styles/index.sass';

class App extends React.Component {
  render() {
    return (
      <Container fluid>
        <Row className='navbar-row'>
          <NavBarMenu />
        </Row>
        <Row className='content-row'>
          <Col md={2} className='navmenu-col'>
            <NavMenu />
          </Col>
          <Col md={10} className='content-col'>
            <RouteSwitch />
          </Col>
        </Row>
      </Container>
    );
  }
}

export default App;