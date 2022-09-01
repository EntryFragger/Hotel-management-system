import axios from "axios";
import qs from 'qs';

axios.defaults.headers.post['Content-Type'] = 'application/json';

axios.defaults.withCredentials = true;

const service = axios.create({
    //baseURL: process.env.VUE_APP_BASE_API, // url = base url + request url
    baseURL:'/api',
    // withCredentials: true, // send cookies when cross-domain requests
    timeout: 50000, // request timeout
    async:true,
    crossDomain:true,
  })
  
  // request interceptor
  service.interceptors.request.use(
      config => {
        if(config.method === 'post') {
          config.data =qs.stringify(config.data)
          config.url += '?' + config.data 
        }
        return config;
      },
      error => {
        // do something with request error
        console.log(error) // for debug
        return Promise.reject(error)
      }
  )
  
  
  // response interceptor
  service.interceptors.response.use(
      /**
       * If you want to get http information such as headers or status
       * Please return  response => response
       */
  
      /**
       * Determine the request status by custom code
       * Here is just an example
       * You can also judge the status by HTTP Status Code
       */
      response => {
        const res = response.data
        console.log('真实的回复为：',response)
        // if the custom code is not 200, it is judged as an error.
        return res
      },
      error => {
        return Promise.reject(error)
      }
  
  )

export default service