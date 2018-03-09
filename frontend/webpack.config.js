module.exports = {
  entry: {
    'account/login/index': "./src/account/login/index.js"
  },
  output: {
    path: __dirname + "../../backend/Crm/wwwroot/dist/",
    filename: "[name].js"
  },
  module: {
    rules: [
      {
        test: /\.js$/,
        exclude: /node_modules/,
        use: {
          loader: "babel-loader"
        }
      }
    ]
  }
}