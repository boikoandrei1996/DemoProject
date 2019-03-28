import React from 'react';
import { connect } from 'react-redux';
import { Alert } from 'react-bootstrap';

class MainHomePage extends React.Component {
  render() {
    const id = this.props.match.params.id;

    return (
      <Alert variant='info'>
        MainView {id}
      </Alert>
    );
  }
}

const connectedComponent = connect()(MainHomePage);
export { connectedComponent as MainHomePage };