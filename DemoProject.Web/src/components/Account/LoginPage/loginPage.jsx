import React from 'react';
import config from 'config';
import { connect } from 'react-redux';
import { ToastContainer } from 'react-toastify';
import { Redirect } from 'react-router-dom';
import { Row, Col } from 'react-bootstrap';
import { RoutePath } from '@/_constants';
import { Title, LoadingSpinner, ErrorAlert } from '@/components/_shared';
import { userActions } from '@/_actions';
import { userService } from '@/_services';
import { LoginForm } from './loginForm';

class LoginPage extends React.Component {
  constructor(props) {
    super(props);
  }

  render() {
    const { loggingIn, errors, loginUser, resetLoginForm } = this.props;

    // redirect to main home if already logged in
    const loggedInUser = userService.getCurrentUser();
    if (loggedInUser) {
      return <Redirect to={RoutePath.MAIN} />;
    }

    return (
      <React.Fragment>
        <Title content='Login Page' />
        {loggingIn && <LoadingSpinner />}
        {errors && errors.map((error, index) => <ErrorAlert key={index} error={error} />)}
        <Row>
          <Col md={{ span: 6, offset: 3 }}>
            {<LoginForm submitForm={loginUser} resetForm={resetLoginForm} />}
          </Col>
        </Row>
        <ToastContainer autoClose={config.toastAutoClose} draggable={false} />
      </React.Fragment>
    );
  }
}

function mapStateToProps(state) {
  return {
    loggingIn: state.getIn(['userState', 'loggingIn']),
    errors: state.getIn(['userState', 'errors'])
  };
}

const connectedComponent = connect(mapStateToProps, userActions)(LoginPage);
export { connectedComponent as LoginPage };