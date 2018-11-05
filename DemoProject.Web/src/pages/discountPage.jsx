import React from 'react';
import Title from '../components/shared/title.jsx';
import DiscountAddForm from '../components/discount/discountAddForm.jsx';
import DiscountList from '../components/discount/discountList.jsx';

class DiscountPage extends React.Component {
  render() {
    return (
      <div>
        <Title content='Discount Page Title' />
        <DiscountAddForm />
        <DiscountList />
      </div>
    );
  }
}

export default DiscountPage;