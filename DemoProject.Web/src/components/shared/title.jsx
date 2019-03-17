import React from 'react';
import { Col, Row } from 'react-bootstrap';
import './title.sass';

class Title extends React.Component {
  render() {
    return (
      <div>
        <Row>
          <Col sm={3} />
          <Col sm={6} align='center'>
            <h1>
              {this.props.content}
            </h1>
          </Col>
          <Col sm={3} />
        </Row>
        <hr />
      </div>
    );
  }
}

export default Title;