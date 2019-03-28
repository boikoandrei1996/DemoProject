import React from 'react';
import { ButtonGroup, Button } from 'react-bootstrap';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import * as icons from '@fortawesome/free-solid-svg-icons';
import { LoadingSpinner } from '@/components/_shared';

class UserItem extends React.Component {
  constructor(props) {
    super(props);

    this.onClickDelete = this.onClickDelete.bind(this);
  }

  onClickDelete() {
    const id = this.props.user.get('id');
    return this.props.deleteUser(id);
  }

  render() {
    const { index, user, loading, loadingId } = this.props;
    const dateOfCreation = new Date(user.get('dateOfCreation')).toLocaleDateString();
    const confirmed = user.get('emailConfirmed');
    const currentLoading = loading && user.get('id') === loadingId;

    return (
      <tr>
        <td>{index + 1}</td>
        <td>{user.get('username')}</td>
        <td>{user.get('role')}</td>
        <td>{user.get('email')}</td>
        <td className={confirmed ? 'bg-success' : 'bg-danger'}>
          {confirmed ? <FontAwesomeIcon icon={icons.faCheckCircle} /> : <FontAwesomeIcon icon={icons.faTimesCircle} />}
        </td>
        <td>{user.get('phoneNumber')}</td>
        <td>{dateOfCreation}</td>
        <td>
          <ButtonGroup aria-label='user-item-actions'>
            {
              currentLoading ?
                <Button variant='outline-danger' onClick={this.onClickDelete}><LoadingSpinner /></Button> :
                <Button variant='danger' onClick={this.onClickDelete}>Удалить <FontAwesomeIcon icon={icons.faTrash} /></Button>
            }
          </ButtonGroup>
        </td>
      </tr>
    );
  }
}

export { UserItem };