import React from 'react';
import { Spinner } from 'react-bootstrap';

class LoadingSpinner extends React.Component {
  render() {
    return (
      <Spinner animation='border' variant='primary' role='status'>
        <span className='sr-only'>Loading...</span>
      </Spinner>
    );
  }
}

export { LoadingSpinner };