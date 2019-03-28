import React from 'react';
import { connect } from 'react-redux';
import { phoneActions } from '@/_actions';
import { PhoneForm, PhonesList } from '@/components/Admin';

class AdminPage extends React.Component {
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
    phones: state.get('phoneState').get('phones')
  };
}

const connectedComponent = connect(mapStateToProps, phoneActions)(AdminPage);
export { connectedComponent as AdminPage };