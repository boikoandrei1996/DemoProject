import React from 'react';
import PhoneItem from "./phoneItem.jsx";

class PhonesList extends React.Component {
  constructor(props) {
    super(props);
  }

  render() {
    return <div>
      {this.props.phones.map(item =>
        <PhoneItem key={item}
          text={item}
          deletePhone={this.props.deletePhone} />
      )}
    </div>
  }
};

export default PhonesList;