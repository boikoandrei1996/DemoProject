import React from 'react';
import { connect } from 'react-redux';
import { Row, Col } from 'react-bootstrap';
import { Title, LoadingSpinner, ErrorAlert, Filter } from '@/components/_shared';
import { UsersList } from './usersList';
import { userActions } from '@/_actions';
import { filterItems } from '@/_helpers';

class UsersPageAll extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      filter: '',
      fields: {
        username: true,
        role: true,
        email: true
      }
    };

    this.handleFilter = this.handleFilter.bind(this);
  }

  componentDidMount() {
    this.props.getAll();
    // TODO: will be updated when API return object after add or update
    //if (!this.props.users) {
    //  this.props.getAll();
    //}
  }

  handleFilter(event) {
    const { name, value, type, checked } = event.target;

    if (type === 'checkbox') {
      this.setState(prevState => ({
        fields: {
          ...prevState.fields,
          [name]: checked
        }
      }));
    }
    else {
      this.setState({
        [name]: value && value.toLowerCase()
      });
    }
  }

  render() {
    const { loading, errors, users } = this.props;
    const { filter, fields } = this.state;
    const checkedkeys = Object.keys(fields).filter(key => fields[key] === true);

    const filteredUsers = filterItems(users, checkedkeys, filter);

    return (
      <div>
        <Title content='All Users' />
        <Row>
          <Col md={4}>
            {loading && <LoadingSpinner />}
          </Col>
          <Col md={{ span: 4, offset: 4 }}>
            <Filter name='filter' value={filter} placeholder='search' fields={fields} onChange={this.handleFilter} />
          </Col>
        </Row>
        {errors && errors.map((error, index) => <ErrorAlert key={index} error={error} />)}
        {filteredUsers && <UsersList users={filteredUsers} removeUser={this.props.removeUser} />}
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