var path = require('path');

module.exports = {
  entry: "./src/index.jsx", // start point
  output: {
    path: path.resolve(__dirname, './public'),
    publicPath: '/public/',
    filename: "bundle.js"
  },
  module: {
    rules: [
      {
        test: /\.jsx?$/,
        exclude: /(node_modules)/,
        loader: "babel-loader",
        options: {
          presets: [
            "@babel/preset-env",
            "@babel/preset-react"
          ]
        }
      }
    ]
  }
}