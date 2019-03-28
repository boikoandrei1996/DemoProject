import React from 'react';
import { connect } from 'react-redux';
import { Title, LoadingSpinner, ErrorAlert, UsersList } from '@/components/_shared';
import { userActions } from '../../_actions';

class UsersPage extends React.Component {
  componentDidMount() {
    this.props.getAll();
  }

  render() {
    const { loading, users, error } = this.props;

    return (
      <div>
        <Title content='All Users' />
        {loading && <LoadingSpinner />}
        {error && <ErrorAlert error={error} />}
        {users && <UsersList users={users} />}
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

const connectedComponent = connect(mapStateToProps, userActions)(UsersPage);
export { connectedComponent as UsersPage };