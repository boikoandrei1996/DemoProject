import React from 'react';
import { LinkContainer } from 'react-router-bootstrap';
import { Nav } from 'react-bootstrap';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import * as icons from '@fortawesome/free-solid-svg-icons'

class NavMenu extends React.Component {
  render() {
    return (
      <Nav variant='pills' className="flex-column">
        <LinkContainer to='/' exact>
          <Nav.Link>
            Home <FontAwesomeIcon icon={icons.faHome} />
          </Nav.Link>
        </LinkContainer>

        <LinkContainer to='/discount'>
          <Nav.Link>
            Discount <FontAwesomeIcon icon={icons.faInfo} />
          </Nav.Link>
        </LinkContainer>

        <LinkContainer to='/temp/1'>
          <Nav.Link>
            About <FontAwesomeIcon icon={icons.faQuestionCircle} />
          </Nav.Link>
        </LinkContainer>

        <LinkContainer to='/temp/2'>
          <Nav.Link>
            Cart <FontAwesomeIcon icon={icons.faShoppingCart} />
          </Nav.Link>
        </LinkContainer>

        <LinkContainer to='/temp/3'>
          <Nav.Link>
            Delivery <FontAwesomeIcon icon={icons.faTruck} />
          </Nav.Link>
        </LinkContainer>

        <LinkContainer to='/temp/4'>
          <Nav.Link>
            MenuItem <FontAwesomeIcon icon={icons.faBars} />
          </Nav.Link>
        </LinkContainer>

        <LinkContainer to='/temp/5'>
          <Nav.Link>
            ShopItem <FontAwesomeIcon icon={icons.faListAlt} />
          </Nav.Link>
        </LinkContainer>

        <LinkContainer to='/temp/6'>
          <Nav.Link>
            Order <FontAwesomeIcon icon={icons.faInbox} />
          </Nav.Link>
        </LinkContainer>
      </Nav>
    );
  }
}

export { NavMenu };