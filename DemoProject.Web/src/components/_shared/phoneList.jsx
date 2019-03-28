import React from 'react';
import { ListGroup } from 'react-bootstrap';
import { PhoneItem } from "./phoneItem";

class PhonesList extends React.Component {
  constructor(props) {
    super(props);
  }

  render() {
    return (
      <ListGroup>
        {this.props.phones.map(item =>
          <PhoneItem key={item} text={item} deletePhone={this.props.deletePhone} />)}
      </ListGroup>
    );
  }
}

export { PhonesList };