import React from 'react';
import { Button, ListGroupItem } from 'react-bootstrap';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import * as icons from '@fortawesome/free-solid-svg-icons';

class PhoneItem extends React.Component {
  constructor(props) {
    super(props);

    this.onClickDelete = this.onClickDelete.bind(this);
  }

  onClickDelete() {
    return this.props.deletePhone(this.props.text);
  }

  render() {
    return (
      <ListGroupItem style={{ padding: '5px', marginTop: '10px' }}>
        <span style={{ marginRight: '5px' }}>
          <b>{this.props.text}</b>
        </span>
        <Button variant='danger' onClick={this.onClickDelete}>
          Удалить <FontAwesomeIcon icon={icons.faTrash} />
        </Button>
      </ListGroupItem>
    );
  }
}

export { PhoneItem };