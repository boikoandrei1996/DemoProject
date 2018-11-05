var path = require('path');

module.exports = {
  entry: "./src/index.jsx", // start point
  output: {
    path: path.resolve(__dirname, './public'),
    filename: "bundle.js",
    publicPath: '/public/'
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
      },
      {
        test: /\.(sass|scss|css)$/,
        use: [
          'style-loader',
          'css-loader',
          'sass-loader'
        ]
      }
    ]
  },
  devServer: {
    historyApiFallback: true
  }
}