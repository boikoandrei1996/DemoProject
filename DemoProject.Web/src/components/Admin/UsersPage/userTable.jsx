import React from 'react';
import { Map } from 'immutable';
import { Table } from 'react-bootstrap';
import { UserTableHeader } from './userTableHeader';
import { UserTableItem } from "./userTableItem";
import { SortDirectionEnum, sortItems, nextSortDirection } from '@/_helpers'

class UserTable extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      sortableField: {
        name: null,
        value: null
      }
    };

    this.updateSortableField = this.updateSortableField.bind(this);
  }

  updateSortableField(field) {
    const isAlredySorted = this.state.sortableField.name === field;
    const currentDirection = isAlredySorted ? this.state.sortableField.value : SortDirectionEnum.none;
    const nextDirection = nextSortDirection(currentDirection);

    this.setState({
      sortableField: {
        name: field,
        value: nextDirection
      }
    });
  }

  render() {
    const { users, removeUser } = this.props;
    const { sortableField } = this.state;

    const sortedUsers = sortItems(users, sortableField);

    return (
      <Table responsive bordered hover>
        <thead className='thead-dark'>
          <UserTableHeader sortableField={Map(sortableField)} sortUsers={this.updateSortableField} />
        </thead>
        <tbody>
          {
            sortedUsers && sortedUsers.map(
              (user, index) => <UserTableItem key={index} user={user.set('index', index + 1)} removeUser={removeUser} />)
          }
        </tbody>
      </Table>
    );
  }
}

export { UserTable };