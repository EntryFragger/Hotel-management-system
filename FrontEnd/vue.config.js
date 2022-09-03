const { defineConfig } = require('@vue/cli-service')
module.exports = defineConfig({
  transpileDependencies: true,

  devServer: {
    open:true,
    host: "localhost",
    port: 8000, // 端口号
    proxy: {
      '^/api': {
        target: 'http://localhost:9998/api',
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
