import React from 'react';

class Title extends React.Component {
  render() {
    return (
      <div>
        <h1 align='center'>
          {this.props.content}
        </h1>
        <hr />
      </div>
    );
  }
}

export { Title };