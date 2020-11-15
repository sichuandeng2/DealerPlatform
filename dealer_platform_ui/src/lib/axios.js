import axios from 'axios'

axios.defaults.baseURL = "http://localhost:5001/"
axios.interceptors.request.use(
  config=>{
    if(localStorage["dpToken"]){
      config.headers.authorization ="bearer "+localStorage["dpToken"];
    }
    return config;
  },
  error=>{
    return Promise.reject("err");
  }
);
axios.interceptors.response.use(
  response=>{
    return response;
  },
  error=>{
    switch (error.response.status) {
      case 401:
        router.replace({
          path:'/',
          query:{
            redirect:router.currentRoute.fullPath
          }
        })
        break;
    }
  }
)

export default axios;