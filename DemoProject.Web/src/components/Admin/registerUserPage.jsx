import React from 'react';
import { connect } from 'react-redux';
import { Title } from '@/components/_shared';

class RegisterUserPage extends React.Component {
  render() {
    return (
      <div>
        <Title content='Register User Page Title' />
      </div>
    );
  }
}

const connectedComponent = connect()(RegisterUserPage);
export { connectedComponent as RegisterUserPage };