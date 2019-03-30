export function generateArray(length) {
  return Array.from({length: length}, (v, k) => k + 1);
}