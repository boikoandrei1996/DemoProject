import React from 'react';
import { SortableTh } from '@/components/_shared';

class UserTableHeader extends React.Component {
  render() {
    const { sortableField, sortUsers } = this.props;
    const key = sortableField.get('name');
    const sortDirection = sortableField.get('value');

    return (
      <tr>
        <th>#</th>
        <SortableTh text='Username' sortDirection={key === 'username' ? sortDirection : null} onClick={() => sortUsers('username')} />
        <SortableTh text='Role' sortDirection={key === 'role' ? sortDirection : null} onClick={() => sortUsers('role')} />
        <th>Email</th>
        <th>Email Confirmed</th>
        <th>Phone Number</th>
        <SortableTh text='Date Of Creation' sortDirection={key === 'dateOfCreation' ? sortDirection : null} onClick={() => sortUsers('dateOfCreation')} />
        <th>Actions</th>
      </tr >
    );
  }
}

export { UserTableHeader };