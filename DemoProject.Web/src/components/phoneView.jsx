import React from 'react';
import { connect } from 'react-redux';
import actions from '../store/actions';
import PhoneForm from './phoneForm.jsx';
import PhonesList from './phoneList.jsx';

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

export default connect(mapStateToProps, actions)(PhoneView);