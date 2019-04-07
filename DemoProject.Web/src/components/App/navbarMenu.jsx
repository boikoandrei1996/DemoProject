import React from 'react';
import { LinkContainer } from 'react-router-bootstrap';
import { Nav, Navbar } from 'react-bootstrap';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import * as icons from '@fortawesome/free-solid-svg-icons';
import { RoutePath } from '@/_constants';
import { userService } from '@/_services';
import { RoleEnum } from '@/_helpers';

class NavBarMenu extends React.Component {
  render() {
    const loggedInUser = userService.getCurrentUser();
    const isLoggedIn = loggedInUser && loggedInUser.user && loggedInUser.user.role;
    const isAdmin = isLoggedIn && loggedInUser.user.role.toLowerCase() === RoleEnum.Admin;

    return (
      <Navbar fixed='top' bg='dark' variant='dark' expand='lg' collapseOnSelect>
        <Navbar.Brand>DemoProject Web</Navbar.Brand>
        <Navbar.Toggle aria-controls='basic-navbar-nav' />
        <Navbar.Collapse id='basic-navbar-nav' className='justify-content-center'>
          <Nav className='mr-auto'>
            <LinkContainer to={RoutePath.MAIN}>
              <Nav.Link>
                <FontAwesomeIcon icon={icons.faHome} /> Main
              </Nav.Link>
            </LinkContainer>

            {
              isLoggedIn ? (
                <LinkContainer to={RoutePath.ACCOUNT}>
                  <Nav.Link>
                    <FontAwesomeIcon icon={icons.faUserCircle} /> Account
                  </Nav.Link>
                </LinkContainer>
              ) : null
            }

            {
              isAdmin ? (
                <LinkContainer to={RoutePath.ADMIN}>
                  <Nav.Link>
                    <FontAwesomeIcon icon={icons.faUserShield} /> Admin
                  </Nav.Link>
                </LinkContainer>
              ) : null
            }
          </Nav>

          <Nav>
            {
              isLoggedIn ?
                (
                  <LinkContainer to={RoutePath.LOGOUT}>
                    <Nav.Link>
                      <FontAwesomeIcon icon={icons.faSignOutAlt} /> Logout {loggedInUser.user.username}
                    </Nav.Link>
                  </LinkContainer>
                ) :
                (
                  <LinkContainer to={RoutePath.LOGIN}>
                    <Nav.Link>
                      <FontAwesomeIcon icon={icons.faSignInAlt} /> Login
                  </Nav.Link>
                  </LinkContainer>
                )
            }
          </Nav>
        </Navbar.Collapse>
      </Navbar>
    );
  }
}

export { NavBarMenu };