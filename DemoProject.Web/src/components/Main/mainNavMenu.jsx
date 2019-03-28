import React from 'react';
import { LinkContainer } from 'react-router-bootstrap';
import { Nav } from 'react-bootstrap';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import * as icons from '@fortawesome/free-solid-svg-icons';
import { RoutePath } from '@/_helpers';

class MainNavMenu extends React.PureComponent {
  render() {
    return (
      <Nav variant='pills' className="flex-column">
        <LinkContainer to={RoutePath.MAIN} exact>
          <Nav.Link>
            Home <FontAwesomeIcon icon={icons.faHome} />
          </Nav.Link>
        </LinkContainer>

        <LinkContainer to={RoutePath.MAIN_ABOUT} exact>
          <Nav.Link>
            About <FontAwesomeIcon icon={icons.faQuestionCircle} />
          </Nav.Link>
        </LinkContainer>

        <LinkContainer to={RoutePath.MAIN_CART} exact>
          <Nav.Link>
            Cart <FontAwesomeIcon icon={icons.faShoppingCart} />
          </Nav.Link>
        </LinkContainer>

        <LinkContainer to={RoutePath.MAIN_DELIVERY} exact>
          <Nav.Link>
            Delivery <FontAwesomeIcon icon={icons.faTruck} />
          </Nav.Link>
        </LinkContainer>

        <LinkContainer to={RoutePath.MAIN_MENUITEM} exact>
          <Nav.Link>
            MenuItem <FontAwesomeIcon icon={icons.faBars} />
          </Nav.Link>
        </LinkContainer>

        <LinkContainer to={RoutePath.MAIN_SHOPITEM} exact>
          <Nav.Link>
            ShopItem <FontAwesomeIcon icon={icons.faListAlt} />
          </Nav.Link>
        </LinkContainer>

        <LinkContainer to={RoutePath.MAIN_ORDER} exact>
          <Nav.Link>
            Order <FontAwesomeIcon icon={icons.faInbox} />
          </Nav.Link>
        </LinkContainer>
      </Nav>
    );
  }
}

export { MainNavMenu };