import React from 'react';
import { Link } from 'react-router-dom';
import { Glyphicon, Nav, Navbar, NavItem } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
import '../styles/navMenu.sass';

class NavMenu extends React.Component {
  render() {
    return (
      <Navbar inverse fixedTop fluid collapseOnSelect>
        <Navbar.Header>
          <Navbar.Brand>
            <Link to='/'>DemoProject Web</Link>
          </Navbar.Brand>
          <Navbar.Toggle />
        </Navbar.Header>
        <Navbar.Collapse>
          <Nav>
            <LinkContainer to='/' exact>
              <NavItem>
                <Glyphicon glyph='home' /> Home
              </NavItem>
            </LinkContainer>
            <LinkContainer to='/temp/3'>
              <NavItem>
                <Glyphicon glyph='question-sign' /> About
              </NavItem>
            </LinkContainer>
            <LinkContainer to='/temp/6'>
              <NavItem>
                <Glyphicon glyph='shopping-cart' /> Cart
              </NavItem>
            </LinkContainer>
            <LinkContainer to='/temp/2'>
              <NavItem>
                <Glyphicon glyph='usd' /> Delivery
              </NavItem>
            </LinkContainer>
            <LinkContainer to='/temp/1'>
              <NavItem>
                <Glyphicon glyph='info-sign' /> Discount
              </NavItem>
            </LinkContainer>
            <LinkContainer to='/temp/4'>
              <NavItem>
                <Glyphicon glyph='cutlery' /> MenuItem
              </NavItem>
            </LinkContainer>
            <LinkContainer to='/temp/7'>
              <NavItem>
                <Glyphicon glyph='inbox' /> Order
              </NavItem>
            </LinkContainer>
            <LinkContainer to='/temp/5'>
              <NavItem>
                <Glyphicon glyph='th-list' /> ShopItem
              </NavItem>
            </LinkContainer>
          </Nav>
        </Navbar.Collapse>
      </Navbar>
    );
  }
}

export default NavMenu;