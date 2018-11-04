import React from 'react';

class TempView extends React.Component {
  render() {
    const id = this.props.match.params.id;
    return <div>TempView {id}</div>
  }
};

export default TempView;