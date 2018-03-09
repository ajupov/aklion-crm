module.exports = {
  entry: {
    index: "./src/index.js"
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