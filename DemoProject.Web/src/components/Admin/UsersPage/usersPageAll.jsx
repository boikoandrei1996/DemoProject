import React from 'react';
import { connect } from 'react-redux';
import { Title, LoadingSpinner, ErrorAlert } from '@/components/_shared';
import { UsersList } from './usersList';
import { userActions } from '@/_actions';

class UsersPageAll extends React.Component {
  componentDidMount() {
    if (!this.props.users) {
      this.props.getAll();
    }
  }

  render() {
    const { loading, error, users } = this.props;

    return (
      <div>
        <Title content='All Users' />
        {loading && <LoadingSpinner />}
        {error && <ErrorAlert error={error} />}
        {users && <UsersList users={users} removeUser={this.props.removeUser} />}
      </div>
    );
  }
}

function mapStateToProps(state) {
  return {
    loading: state.getIn(['userState', 'loading']),
    users: state.getIn(['userState', 'users']),
    error: state.getIn(['userState', 'error'])
  };
}

const connectedComponent = connect(mapStateToProps, userActions)(UsersPageAll);
export { connectedComponent as UsersPageAll };