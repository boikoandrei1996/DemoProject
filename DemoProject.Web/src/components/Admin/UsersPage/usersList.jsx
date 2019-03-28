import React from 'react';
import { Table } from 'react-bootstrap';
import { UserItem } from "./userItem";

class UsersList extends React.Component {
  render() {
    return (
      <Table responsive bordered hover>
        <thead className='thead-dark'>
          <tr>
            <th>#</th>
            <th>Username</th>
            <th>Role</th>
            <th>Email</th>
            <th>Email Confirmed</th>
            <th>Phone Number</th>
            <th>Date Of Creation</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {this.props.users.map((user, index) => <UserItem key={user.get('id')} user={user} index={index} {...this.props} />)}
        </tbody>
      </Table>
    );
  }
}

export { UsersList };