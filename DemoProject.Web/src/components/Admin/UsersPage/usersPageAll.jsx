import React from 'react';
import { connect } from 'react-redux';
import { Title, LoadingSpinner, ErrorAlert } from '@/components/_shared';
import { UsersList } from './usersList';
import { userActions } from '@/_actions';

class UsersPageAll extends React.Component {
  componentDidMount() {
    this.props.getAll();
    // TODO: will be updated when API return object after add or update
    //if (!this.props.users) {
    //  this.props.getAll();
    //}
  }

  render() {
    const { loading, errors, users } = this.props;

    return (
      <div>
        <Title content='All Users' />
        {loading && <LoadingSpinner />}
        {errors && errors.map((error, index) => <ErrorAlert key={index} error={error} />)}
        {users && <UsersList users={users} removeUser={this.props.removeUser} />}
      </div>
    );
  }
}

function mapStateToProps(state) {
  return {
    loading: state.getIn(['userState', 'loading']),
    users: state.getIn(['userState', 'users']),
    errors: state.getIn(['userState', 'errors'])
  };
}

const connectedComponent = connect(mapStateToProps, userActions)(UsersPageAll);
export { connectedComponent as UsersPageAll };