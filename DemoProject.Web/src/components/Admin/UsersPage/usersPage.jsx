import React from 'react';
import { connect } from 'react-redux';
import { generateArray, history } from '@/_helpers';
import { Title, LoadingSpinner, ErrorAlert, MyPagination } from '@/components/_shared';
import { UsersList } from './usersList';
import { userActions } from '@/_actions';
import { RoutePath } from '@/_constants';

class UsersPage extends React.Component {
  constructor(props) {
    super(props);

    this.onClickPage = this.onClickPage.bind(this);
  }

  onClickPage(index) {
    history.push(RoutePath.ADMIN_USERS_PAGE + '/' + index);
    return this.props.getPage(index);
  }

  componentDidMount() {
    const index = this.props.match.params.index || 1;
    this.props.getPage(index);
  }

  render() {
    const pageIndex = Number(this.props.match.params.index) || 1;
    const { loading, errors, users, totalPages } = this.props;

    return (
      <div>
        <Title content='Users Pagination' />
        {loading && <LoadingSpinner />}
        {errors && errors.map((error, index) => <ErrorAlert key={index} error={error} />)}
        {users && <UsersList users={users} removeUser={this.props.removeUser} />}
        {users && <MyPagination items={generateArray(totalPages)} active={pageIndex} onClick={this.onClickPage} />}
      </div>
    );
  }
}

function mapStateToProps(state) {
  return {
    loading: state.getIn(['userState', 'loading']),
    users: state.getIn(['userState', 'users']),
    totalPages: state.getIn(['userState', 'totalPages']) || 1,
    errors: state.getIn(['userState', 'errors'])
  };
}

const connectedComponent = connect(mapStateToProps, userActions)(UsersPage);
export { connectedComponent as UsersPage };