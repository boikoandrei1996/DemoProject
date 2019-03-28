import React from 'react';
import { connect } from 'react-redux';
import { Alert, ListGroup, ListGroupItem } from 'react-bootstrap';
import { Title } from '@/components/_shared';
import { userActions } from '../../_actions';

class UsersPage extends React.Component {
  componentDidMount() {
    this.props.getAll();
  }

  render() {
    const { loading, users, error } = this.props;

    return (
      <div>
        <Title content='Users Page Title' />
        {loading && <em>Loading...</em>}
        {error && <Alert variant='danger'>ERROR: {error}</Alert>}
        {users &&
          <ListGroup>
            {users.map((user, index) => (
              <ListGroupItem key={user.get('id')}>
                {index + 1}. {user.get('username')}
              </ListGroupItem>))}
          </ListGroup>
        }
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