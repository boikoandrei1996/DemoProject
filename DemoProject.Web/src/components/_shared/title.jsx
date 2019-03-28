import React from 'react';
import '@/_styles/app.sass';

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