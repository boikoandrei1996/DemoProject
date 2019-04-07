import React from 'react';
import { connect } from 'react-redux';
import { Redirect } from 'react-router-dom';
import { RoutePath } from '@/_constants';
import { userActions } from '@/_actions';

class LogoutPage extends React.Component {
  render() {
    this.props.dispatch(userActions.logoutUser());

    return <Redirect to={RoutePath.LOGIN} />;
  }
}

const connectedComponent = connect()(LogoutPage);
export { connectedComponent as LogoutPage };