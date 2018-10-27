import React from 'react';

class PhoneForm extends React.Component {
  constructor(props) {
    super(props);

    this.onClickAdd = this.onClickAdd.bind(this);
  }

  onClickAdd() {
    if (this.refs.phoneInput.value !== "") {
      var itemText = this.refs.phoneInput.value;
      this.refs.phoneInput.value = "";
      return this.props.addPhone(itemText);
    }
  }

  render() {
    return <div>
      <input ref="phoneInput" />
      <button onClick={this.onClickAdd}>Добавить</button>
    </div>
  }
};

export default PhoneForm;