import React from 'react';
import config from 'config';
import { connect } from 'react-redux';
import { ToastContainer } from 'react-toastify';
import { Title, LoadingSpinner, ErrorAlert } from '@/components/_shared';
import { userActions } from '@/_actions';
import { RegisterUserForm } from './registerUserForm';

class RegisterUserPage extends React.Component {
  render() {
    const { registering, error } = this.props;

    return (
      <div>
        <Title content='Register User Page' />
        {registering && <LoadingSpinner />}
        {error && <ErrorAlert error={error} />}
        {<RegisterUserForm submitUser={this.props.registerUser} resetForm={this.props.resetRegisterUserForm} />}
        <ToastContainer autoClose={config.toastAutoClose} draggable={false} />
      </div>
    );
  }
}

function mapStateToProps(state) {
  return {
    registering: state.getIn(['userState', 'registering']),
    error: state.getIn(['userState', 'error'])
  };
}

const connectedComponent = connect(mapStateToProps, userActions)(RegisterUserPage);
export { connectedComponent as RegisterUserPage };