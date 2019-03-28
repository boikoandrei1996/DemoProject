import React from 'react';
import { LinkContainer } from 'react-router-bootstrap';
import { Nav, Navbar } from 'react-bootstrap';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import * as icons from '@fortawesome/free-solid-svg-icons';
import { RoutePath } from '@/_constants';

class NavBarMenu extends React.Component {
  render() {
    return (
      <Navbar fixed='top' bg='dark' variant='dark' expand='lg' collapseOnSelect>
        <Navbar.Brand>DemoProject Web</Navbar.Brand>
        <Navbar.Toggle aria-controls='basic-navbar-nav' />
        <Navbar.Collapse id='basic-navbar-nav' className='justify-content-center'>
          <Nav>
            <LinkContainer to={RoutePath.MAIN}>
              <Nav.Link>
                <FontAwesomeIcon icon={icons.faHome} /> Main
              </Nav.Link>
            </LinkContainer>

            <LinkContainer to={RoutePath.ACCOUNT}>
              <Nav.Link>
                <FontAwesomeIcon icon={icons.faUserCircle} /> Account
              </Nav.Link>
            </LinkContainer>

            <LinkContainer to={RoutePath.ADMIN}>
              <Nav.Link>
                <FontAwesomeIcon icon={icons.faUserShield} /> Admin
              </Nav.Link>
            </LinkContainer>
          </Nav>
        </Navbar.Collapse>
      </Navbar>
    );
  }
}

export { NavBarMenu };