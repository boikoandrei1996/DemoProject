import React from 'react';
import { connect } from 'react-redux';
import { Title } from '@/components/_shared';

class AccountHomePage extends React.Component {
  render() {
    return (
      <div>
        <Title content='Account Page Title' />
      </div>
    );
  }
}

const connectedComponent = connect()(AccountHomePage);
export { connectedComponent as AccountHomePage };