import React from 'react';
import { connect } from 'react-redux';

class MainPage extends React.Component {
  render() {
    const id = this.props.match.params.id;
    return <div>MainView {id}</div>;
  }
}

const connectedComponent = connect()(MainPage);
export { connectedComponent as MainPage };