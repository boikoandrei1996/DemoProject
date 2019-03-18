import React from 'react';
import { connect } from 'react-redux';

class TempPage extends React.Component {
  render() {
    const id = this.props.match.params.id;
    return <div>TempView {id}</div>;
  }
}

const connectedComponent = connect()(TempPage);
export { connectedComponent as TempPage };