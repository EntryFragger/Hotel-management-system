const { defineConfig } = require('@vue/cli-service')
module.exports = defineConfig({
  transpileDependencies: true,

  devServer: {
    open:true,
    host: "localhost",
    port: 8080, // 端口号
    proxy: {
      '^/api': {
        target: 'http://localhost:27187/api',
        ws: true,
        changeOrigin: true,
        pathRewrite:{'^/api':''}
      }/*,
      '^/foo': {
        target: '<other_url>'
      }*/
    }
  }
})
