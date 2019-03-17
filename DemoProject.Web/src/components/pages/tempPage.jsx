import React from 'react';
import { connect } from 'react-redux';

class TempPage extends React.Component {
  render() {
    const id = this.props.match.params.id;
    return <div>TempView {id}</div>;
  }
}

function mapStateToProps(state, props) {
  console.log(state);
  console.log(props);
  return {
    // phones: state.get('phones')
  };
}

const connectedComponent = connect(mapStateToProps)(TempPage);
export { connectedComponent as TempPage };