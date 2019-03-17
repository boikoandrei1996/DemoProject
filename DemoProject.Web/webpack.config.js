var path = require('path');

module.exports = {
  // mode: 'development',
  entry: "./src/index.jsx",
  output: {
    path: path.resolve(__dirname, './public'),
    filename: "bundle.js",
    publicPath: '/public/'
  },
  devServer: {
    historyApiFallback: true,
    port: 8080,
    contentBase: 'public/'
    // publicPath: '/' // Live-reload
  },
  resolve: {
    extensions: ['.js', '.jsx'],
    alias: {
      '@': path.resolve(__dirname, 'src/')
    }
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
      },
      {
        test: /\.woff2?(\?v=[0-9]\.[0-9]\.[0-9])?$/,
        use: 'url-loader?limit=10000'
      },
      {
        test: /\.(ttf|eot|svg)(\?[\s\S]+)?$/,
        use: 'file-loader'
      },
      {
        test: /\.(jpe?g|png|gif|svg)$/i,
        use: 'file-loader?name=images/[name].[ext]'
      }
    ]
  },
  externals: {
    // global app config object
    config: JSON.stringify({
      apiUrl: 'http://localhost:4000'
    })
  }
};