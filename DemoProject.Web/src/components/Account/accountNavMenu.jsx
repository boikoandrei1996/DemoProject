import React from 'react';
import { LinkContainer } from 'react-router-bootstrap';
import { Nav } from 'react-bootstrap';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import * as icons from '@fortawesome/free-solid-svg-icons';
import { RoutePath } from '@/_constants';

class AccountNavMenu extends React.Component {
  render() {
    return (
      <Nav variant='pills' className='flex-column'>
        <LinkContainer to={RoutePath.ACCOUNT} exact>
          <Nav.Link>
            Home <FontAwesomeIcon icon={icons.faHome} />
          </Nav.Link>
        </LinkContainer>
      </Nav>
    );
  }
}

export { AccountNavMenu };