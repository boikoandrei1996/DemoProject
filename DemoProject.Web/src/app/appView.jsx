import React from 'react';
import { connect } from "react-redux";
import actions from "./actions.jsx";
import PhoneForm from "./components/phoneForm.jsx";
import PhonesList from "./components/phoneList.jsx";

class AppView extends React.Component {
  render() {
    return <div>
      <PhoneForm addPhone={this.props.addPhone} />
      <PhonesList {...this.props} />
    </div>
  }
};

function mapStateToProps(state) {
  return {
    phones: state.get("phones")
  };
}

export default connect(mapStateToProps, actions)(AppView);