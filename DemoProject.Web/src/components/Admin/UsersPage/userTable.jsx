import React from 'react';
import { Table } from 'react-bootstrap';
import { UserTableItem } from "./userTableItem";

class UserTable extends React.Component {
  constructor(props) {
    super(props);
  }

  render() {
    const { users, removeUser } = this.props;

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
          {
            users && users.map(
              (user, index) => <UserTableItem key={index} user={user.set('index', index + 1)} removeUser={removeUser} />)
          }
        </tbody>
      </Table>
    );
  }
}

export { UserTable };