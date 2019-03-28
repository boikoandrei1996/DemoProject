import React from 'react';
import { Button, ListGroupItem } from 'react-bootstrap';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import * as icons from '@fortawesome/free-solid-svg-icons';
import '@/_styles/userItem.sass';

class UserItem extends React.Component {
  constructor(props) {
    super(props);

    this.onClickDelete = this.onClickDelete.bind(this);
    this.onClickDetails = this.onClickDetails.bind(this);
  }

  onClickDelete() {
    alert('delete');
  }

  onClickDetails() {
    alert('details');
  }

  render() {
    const { index, user } = this.props;

    return (
      <ListGroupItem key={user.get('id')} className='user-item'>
        <span class='user-item-content'>{index + 1}. {user.get('username')}</span>
        <Button variant='info' onClick={this.onClickDetails}>
          Подробнее <FontAwesomeIcon icon={icons.faInfo} />
        </Button>
        <Button variant='danger' onClick={this.onClickDelete}>
          Удалить <FontAwesomeIcon icon={icons.faTrash} />
        </Button>
      </ListGroupItem>
    );
  }
}

export { UserItem };