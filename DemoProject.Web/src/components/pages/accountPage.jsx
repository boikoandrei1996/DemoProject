import React from 'react';
import { connect } from 'react-redux';
import { Title } from '@/components/_shared';

class AccountPage extends React.Component {
  render() {
    return (
      <div>
        <Title content='Account Page Title' />
      </div>
    );
  }
}

const connectedComponent = connect()(AccountPage);
export { connectedComponent as AccountPage };