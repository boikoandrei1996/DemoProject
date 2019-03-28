import React from 'react';
import { Alert } from 'react-bootstrap';

class ErrorAlert extends React.Component {
  render() {
    const message = this.props.error || this.props.errorMessage;

    return (
      <Alert variant='danger'>Error: {message}</Alert>
    );
  }
}

export { ErrorAlert };