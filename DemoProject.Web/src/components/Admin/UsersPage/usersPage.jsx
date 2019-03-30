import React from 'react';
import { connect } from 'react-redux';
import { generateArray } from '@/_helpers';
import { Title, LoadingSpinner, ErrorAlert, MyPagination } from '@/components/_shared';
import { UsersList } from './usersList';
import { userActions } from '@/_actions';

class UsersPage extends React.Component {
  componentDidMount() {
    this.props.getPage(1);
  }

  render() {
    const { loading, error, page } = this.props;

    return (
      <div>
        <Title content='Users Pagination' />
        {loading && <LoadingSpinner />}
        {error && <ErrorAlert error={error} />}
        {page && <UsersList users={page.get('records')} removeUser={this.props.removeUser} />}
        {page && <MyPagination items={generateArray(page.get('totalPages'))} active={page.get('currentPage')} onClick={this.props.getPage} />}
      </div>
    );
  }
}

function mapStateToProps(state) {
  return {
    loading: state.getIn(['userState', 'loading']),
    page: state.getIn(['userState', 'page']),
    error: state.getIn(['userState', 'error'])
  };
}

const connectedComponent = connect(mapStateToProps, userActions)(UsersPage);
export { connectedComponent as UsersPage };