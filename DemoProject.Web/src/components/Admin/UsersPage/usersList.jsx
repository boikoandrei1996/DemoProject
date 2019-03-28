import React from 'react';
import { ListGroup } from 'react-bootstrap';
import { UserItem } from "./userItem";

class UsersList extends React.Component {
  render() {
    const { users } = this.props;

    return (
      <ListGroup>
        {users.map((user, index) => <UserItem user={user} index={index} />)}
      </ListGroup>
    );
  }
}

export { UsersList };