import React from 'react';
import { Alert } from 'react-bootstrap';

class NotFoundPage extends React.Component {
  render() {
    return (
      <Alert variant='danger'>
        <Alert.Heading>Ooops...</Alert.Heading>
        <p>Page not found</p>
      </Alert>
    );
  }
}

export { NotFoundPage };