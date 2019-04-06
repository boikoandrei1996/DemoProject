import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import * as icons from '@fortawesome/free-solid-svg-icons';
import { SortDirectionEnum } from '@/_helpers';
import '@/_styles/app.sass';

class SortableTh extends React.Component {
  render() {
    const { text, sortDirection, onClick } = this.props;

    return (
      <th className='clickable' onClick={onClick}>
        {text}
        &nbsp;
        {getIcon(sortDirection)}
      </th>
    );
  }
}

function getIcon(sortDirection) {
  if (sortDirection === SortDirectionEnum.up) {
    return <FontAwesomeIcon icon={icons.faSortUp} />;
  }
  else if (sortDirection === SortDirectionEnum.down) {
    return <FontAwesomeIcon icon={icons.faSortDown} />;
  }
  else {
    return <FontAwesomeIcon icon={icons.faSort} />;
  }
}

export { SortableTh };