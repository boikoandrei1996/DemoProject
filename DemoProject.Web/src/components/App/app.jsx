import React from 'react';
import { Container, Row, Col } from 'react-bootstrap';
import { NavBarMenu } from '.';
import { NavigationSwitch } from '@/components/_navigations';
import { PageSwitch } from '@/components/pages';
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