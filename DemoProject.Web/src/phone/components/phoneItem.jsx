import React from 'react';

class PhoneItem extends React.Component {
  constructor(props) {
    super(props);

    this.onClickDelete = this.onClickDelete.bind(this);
  }

  onClickDelete() {
    return this.props.deletePhone(this.props.text);
  }

  render() {
    return <div>
      <p>
        <b>{this.props.text}</b><br />
        <button onClick={this.onClickDelete}>Удалить</button>
      </p>
    </div>
  }
};

export default PhoneItem;