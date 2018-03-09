module.exports = {
  context: __dirname + "/app",
  entry: {
    accountLogin: "./src/account/login/index.js",
    accountRegister: "./src/account/register/index.js"
  },
  output: {
    path: __dirname + "../../backend/Crm/wwwroot/dist/",
    filename: "[name].min.js"
  }
}