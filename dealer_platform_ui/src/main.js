import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import { Toast, MessageBox } from "mint-ui";
import "mint-ui/lib/style.min.css";
import axios from "./lib/axios";
import VueLazyLoad from "vue-lazyload";
import ElementUI from "element-ui";
import "element-ui/lib/theme-chalk/index.css";

//axios.defaults.headers.authorization=`Bearer ${localStorage["dpToken"]}`;

Vue.config.productionTip = false;
Vue.prototype.$msg = Toast;
Vue.prototype.$alert = MessageBox;
Vue.prototype.$http = axios;

Vue.filter("price", (value) => value.toFixed(2));
Vue.filter("date", (value) => {
  if (value != null) return value.substring(0, value.indexOf("T"));
});
Vue.filter("remark", (value) => (value == "" || value == null ? "无" : value));

Vue.use(ElementUI);
Vue.use(VueLazyLoad, {
  preLoad: 1.3,
  error: "/img/icons/waitImage.jpg",
  loading: "/img/icons/waitImage.jpg",
  attempt: 1,
});
Vue.filter("price", (value) => {
  return value.toFixed(2);
});
new Vue({
  router,
  store,
  render: (h) => h(App),
}).$mount("#app");
