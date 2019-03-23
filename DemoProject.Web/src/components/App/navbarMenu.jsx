import React from 'react';
import { LinkContainer } from 'react-router-bootstrap';
import { Nav, Navbar } from 'react-bootstrap';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import * as icons from '@fortawesome/free-solid-svg-icons';

class NavBarMenu extends React.Component {
  render() {
    return (
      <Navbar fixed='top' bg='dark' variant='dark' expand='lg' collapseOnSelect>
        <Navbar.Brand>DemoProject Web</Navbar.Brand>
        <Navbar.Toggle aria-controls='basic-navbar-nav' />
        <Navbar.Collapse id='basic-navbar-nav' className="justify-content-center">
          <Nav>
            <LinkContainer to='/' exact>
              <Nav.Link>
                <FontAwesomeIcon icon={icons.faHome} /> Home
              </Nav.Link>
            </LinkContainer>

            <LinkContainer to='/discount'>
              <Nav.Link>
                <FontAwesomeIcon icon={icons.faInfo} /> Discount
              </Nav.Link>
            </LinkContainer>

            <LinkContainer to='/temp/1'>
              <Nav.Link>
                <FontAwesomeIcon icon={icons.faQuestionCircle} /> About
              </Nav.Link>
            </LinkContainer>

            <LinkContainer to='/temp/2'>
              <Nav.Link>
                <FontAwesomeIcon icon={icons.faShoppingCart} /> Cart
              </Nav.Link>
            </LinkContainer>

            <LinkContainer to='/temp/3'>
              <Nav.Link>
                <FontAwesomeIcon icon={icons.faTruck} /> Delivery
              </Nav.Link>
            </LinkContainer>

            <LinkContainer to='/temp/4'>
              <Nav.Link>
                <FontAwesomeIcon icon={icons.faBars} /> MenuItem
              </Nav.Link>
            </LinkContainer>

            <LinkContainer to='/temp/5'>
              <Nav.Link>
                <FontAwesomeIcon icon={icons.faListAlt} /> ShopItem
              </Nav.Link>
            </LinkContainer>

            <LinkContainer to='/temp/6'>
              <Nav.Link>
                <FontAwesomeIcon icon={icons.faInbox} /> Order
              </Nav.Link>
            </LinkContainer>

            <LinkContainer to='/temp/10'>
              <Nav.Link>
                <FontAwesomeIcon icon={icons.faUserCircle} /> Account
              </Nav.Link>
            </LinkContainer>

            <LinkContainer to='/temp/11'>
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