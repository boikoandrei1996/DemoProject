import React from 'react';
import { connect } from 'react-redux';
import { Title } from '@/components/_shared';

class UsersPage extends React.Component {
  render() {
    return (
      <div>
        <Title content='Users Page Title' />
      </div>
    );
  }
}

const connectedComponent = connect()(UsersPage);
export { connectedComponent as UsersPage };