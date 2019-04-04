import React from 'react';
import { ButtonGroup, Button } from 'react-bootstrap';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import * as icons from '@fortawesome/free-solid-svg-icons';
import { LoadingSpinner } from '@/components/_shared';

class UserTableItem extends React.Component {
  constructor(props) {
    super(props);

    this.onClickDelete = this.onClickDelete.bind(this);
  }

  onClickDelete() {
    const { user, removeUser } = this.props;
    return removeUser(user.get('id'));
  }

  render() {
    const { user } = this.props;

    const dateOfCreation = new Date(user.get('dateOfCreation')).toLocaleDateString();
    const confirmed = user.get('emailConfirmed');
    const deleting = user.get('deleting');

    return (
      <tr>
        <td>{user.get('index')}</td>
        <td>{user.get('username')}</td>
        <td>{user.get('role')}</td>
        <td>{user.get('email')}</td>
        <td className={confirmed ? 'bg-success' : 'bg-danger'} align='center'>
          <FontAwesomeIcon icon={confirmed ? icons.faCheckCircle : icons.faTimesCircle} />
        </td>
        <td>{user.get('phoneNumber')}</td>
        <td>{dateOfCreation}</td>
        <td>
          <ButtonGroup aria-label='user-item-actions'>
            <Button variant={deleting ? 'outline-danger' : 'danger'} onClick={this.onClickDelete}>
              {
                deleting ?
                  <LoadingSpinner /> :
                  <span>Удалить <FontAwesomeIcon icon={icons.faTrash} /></span>
              }
            </Button>
          </ButtonGroup>
        </td>
      </tr>
    );
  }
}

export { UserTableItem };