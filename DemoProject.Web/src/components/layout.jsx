import React from 'react';
import { Col, Grid, Row } from 'react-bootstrap';
import NavMenu from './shared/navMenu';
import Routes from './routes';

class Layout extends React.Component {
  render() {
    return (
      <Grid fluid>
        <Row>
          <Col sm={3}>
            <NavMenu />
          </Col>
          <Col sm={9}>
            {Routes}
          </Col>
        </Row>
      </Grid>
    );
  }
}

export default Layout;