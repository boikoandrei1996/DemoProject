import React from 'react';
import { connect } from 'react-redux';
import { Title } from '@/components/shared';

class DiscountPage extends React.Component {
  render() {
    return (
      <div>
        <Title content='Discount Page Title' />
      </div>
    );
  }
}

const connectedComponent = connect()(DiscountPage);
export { connectedComponent as DiscountPage };