import { List, Map } from 'immutable';

export const SortDirectionEnum = Object.freeze({
  'none': 0,
  'up': 1,
  'down': 2
});

// none -> down -> up -> none
export function nextSortDirection(current) {
  current = current || SortDirectionEnum.none;

  if (current === SortDirectionEnum.none) {
    return SortDirectionEnum.down;
  }
  else if (current === SortDirectionEnum.down) {
    return SortDirectionEnum.up;
  }
  else if (current === SortDirectionEnum.up) {
    return SortDirectionEnum.none;
  }

  return current;
}

export function sortItems(items, field) {
  const _items = List(items);
  const _field = Map(field);
  const key = _field.get('name');
  const sortDirection = _field.get('value');

  if (_items.isEmpty() || !key || !sortDirection) {
    return _items;
  }

  // Check if item has provided key
  const anyItem = _items.first() || Map();
  if (anyItem.has(key) === false) {
    return _items;
  }

  let sortedItems;
  if (sortDirection === SortDirectionEnum.up) {
    sortedItems = _items.sortBy(x => x.get(key), comparatorUp);
  }
  else if (sortDirection === SortDirectionEnum.down) {
    sortedItems = _items.sortBy(x => x.get(key), comparatorDown);
  }
  else { // SortDirectionEnum.none
    sortedItems = _items;
  }

  return sortedItems;
}

function comparatorDown(a, b) {
  if (a < b) {
    return -1;
  }
  else if (a > b) {
    return 1;
  }
  else {
    return 0;
  }
}

function comparatorUp(a, b) {
  if (a < b) {
    return 1;
  }
  else if (a > b) {
    return -1;
  }
  else {
    return 0;
  }
}