import React from 'react';
import { Switch, Route } from 'react-router-dom';
import { Container, Row } from 'react-bootstrap';

import { NavMenu } from './shared';
import { HomePage, DiscountPage, TempPage, NotFoundPage } from './pages';
import 'bootstrap/dist/css/bootstrap.min.css';
import '@/_styles/index.sass';

import { library } from '@fortawesome/fontawesome-svg-core'
import { faIgloo } from '@fortawesome/free-solid-svg-icons'
library.add(faIgloo);

class App extends React.Component {
  render() {
    return (
      <Container>
        <Row>
          <NavMenu />
        </Row>
        <Row className='main'>
          <Switch>
            <Route exact path='/' component={HomePage} />
            <Route exact path='/discount' component={DiscountPage} />
            <Route exact path='/temp/:id(\d+)' component={TempPage} />
            <Route component={NotFoundPage} />
          </Switch>
        </Row>
      </Container>
    );
  }
}

export default App;