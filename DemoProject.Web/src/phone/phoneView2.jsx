import React from 'react';

class PhoneView2 extends React.Component {
  render() {
    const id = this.props.match.params.id;
    return <div>PhoneView {id}</div>
  }
};

export default PhoneView2;