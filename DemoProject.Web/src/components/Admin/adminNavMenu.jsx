import React from 'react';
import { LinkContainer } from 'react-router-bootstrap';
import { Nav } from 'react-bootstrap';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import * as icons from '@fortawesome/free-solid-svg-icons';
import { RoutePath } from '@/_constants';

class AdminNavMenu extends React.Component {
  render() {
    return (
      <Nav variant='pills' className='flex-column'>
        <LinkContainer to={RoutePath.ADMIN} exact>
          <Nav.Link>
            Home <FontAwesomeIcon icon={icons.faHome} />
          </Nav.Link>
        </LinkContainer>

        <LinkContainer to={RoutePath.ADMIN_USERS} exact>
          <Nav.Link>
            All Users <FontAwesomeIcon icon={icons.faUserFriends} />
          </Nav.Link>
        </LinkContainer>

        <LinkContainer to={RoutePath.ADMIN_REGISTER_USER} exact>
          <Nav.Link>
            Register <FontAwesomeIcon icon={icons.faUserPlus} />
          </Nav.Link>
        </LinkContainer>
      </Nav>
    );
  }
}

export { AdminNavMenu };