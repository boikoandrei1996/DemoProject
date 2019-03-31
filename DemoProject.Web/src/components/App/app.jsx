import React from 'react';
import { Container, Row, Col } from 'react-bootstrap';
import { NavBarMenu, NavigationSwitch, PageSwitch } from '.';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'react-toastify/dist/ReactToastify.min.css';
import '@/_styles/app.sass';

class App extends React.Component {
  render() {
    return (
      <Container fluid>
        <Row className='navbar-row'>
          <NavBarMenu />
        </Row>
        <Row className='content-row'>
          <Col md={2} className='navmenu-col'>
            <NavigationSwitch />
          </Col>
          <Col md={10} className='content-col'>
            <PageSwitch />
          </Col>
        </Row>
      </Container>
    );
  }
}

export { App };