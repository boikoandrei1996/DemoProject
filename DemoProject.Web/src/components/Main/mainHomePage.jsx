import React from 'react';
import { connect } from 'react-redux';

class MainHomePage extends React.Component {
  render() {
    const id = this.props.match.params.id;
    return <div>MainView {id}</div>;
  }
}

const connectedComponent = connect()(MainHomePage);
export { connectedComponent as MainHomePage };