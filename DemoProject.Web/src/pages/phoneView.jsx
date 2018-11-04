import React from 'react';
import { connect } from 'react-redux';
import phoneActions from '../actions/phoneActions';
import PhoneForm from '../components/phoneForm.jsx';
import PhonesList from '../components/phoneList.jsx';

class PhoneView extends React.Component {
  render() {
    return <div>
      <PhoneForm addPhone={this.props.addPhone} />
      <PhonesList {...this.props} />
    </div>
  }
};

function mapStateToProps(state) {
  return {
    phones: state.get('phones')
  };
}

export default connect(mapStateToProps, phoneActions)(PhoneView);