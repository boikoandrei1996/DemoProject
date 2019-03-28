import React from 'react';
import { connect } from 'react-redux';
import { Title, LoadingSpinner, ErrorAlert } from '@/components/_shared';
import { UsersList } from './usersList';
import { userActions } from '@/_actions';

class UsersPage extends React.Component {
  componentDidMount() {
    this.props.getAll();
  }

  render() {
    const { loading, deleting, deletingId, users, error } = this.props;

    return (
      <div>
        <Title content='All Users' />
        {loading && <LoadingSpinner />}
        {error && <ErrorAlert error={error} />}
        {users && <UsersList users={users} loading={deleting} loadingId={deletingId} deleteUser={this.props.remove} />}
      </div>
    );
  }
}

function mapStateToProps(state) {
  return {
    loading: state.getIn(['userState', 'loading']),
    deleting: state.getIn(['userState', 'deleting']),
    deletingId: state.getIn(['userState', 'deletingId']),
    users: state.getIn(['userState', 'users']),
    error: state.getIn(['userState', 'error'])
  };
}

const connectedComponent = connect(mapStateToProps, userActions)(UsersPage);
export { connectedComponent as UsersPage };