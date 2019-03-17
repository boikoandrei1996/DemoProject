import React from 'react';
import { connect } from 'react-redux';
import { phoneActions } from '@/_actions';
import { PhoneForm, PhonesList } from '@/components/phone';

class HomePage extends React.Component {
  render() {
    return (
      <div>
        <PhoneForm addPhone={this.props.addPhone} />
        <PhonesList {...this.props} />
      </div>
    );
  }
}

function mapStateToProps(state) {
  return {
    phones: state.get('phones')
  };
}

const connectedComponent = connect(mapStateToProps, phoneActions)(HomePage);
export { connectedComponent as HomePage };