import Vue from 'vue'
import Vuex from 'vuex'
import axios from '../lib/axios'

Vue.use(Vuex)

export default new Vuex.Store({
  //类比于vue的data
  state: {
    cartNum:10,
    unFinedNum:0
  },
  //类比computed计算属性
  //this.$store.getters.getCartNum;
  getters:{
    getCartNum:state=>state.cartNum,
    getUnFinedNum:state=>state.unFinedNum,
  },
  //类比于vue的methods（同步的）
  //★ this.$store.commit('setCartNum',1);
  mutations: {
    setCartNum:function(state,num){
      state.cartNum = num;
    },
    addCartNum(state,num){
      state.cartNum += num;
    }
  },
  //类比于vue的methods（异步的）
  //★ this.$store.dispatch('addCartNumAsync',1);
  actions: {
    addCartNumAsync:function (context,num) {
      context.commit("addCartNum",num);
    },
    getCartNumAsync:async function(context){
      var res = await axios.get("http://localhost:5001/num");
      context.commit("setCartNum",res.data);
    },
    //this.$store.dispatch('getUnFinishedOrderNumAsync');
    getUnFinishedOrderNumAsync:async function(context){
      var res = await axios.get("http://localhost:5001/orderinfo/unFinNum");
      context.state.unFinedNum = res.data;
    }
  },
  //模块
  modules: {
    user:{
      //在Vuex模块中，不考虑命名空间的情况下，有且只有state需要标注区域
      //this.$store.state.user.userName
      state:{
        userName:"Ace"
      },
      getters:{
        nameGet:function(state){
          //在区域内部调用本区域自身的state是不需要加区域名称
          return state.userName;
        }
      },
      mutations:{
        //★ this.$store.commit('getName',param);
        getName:function(state,param){
           state.userName = param.firstName +" "+state.userName ;
        }
      },
      actions:{
        //★ this.$store.commit('getName',param);
        getNameAsync:function(context,param){
          context.commit('getName',param);
        }
      }
    },
    post:{
      namespaced:true,
      //无论在有无命名空间的区域中，state均不受影响
      //this.$store.state.post.postName
      state:{
        postName:"Ace"
      },
      //getters在设置命名空间的区域中受到的影响最大
      getters:{
        //this.$store.getters["post/nameGet"]
        nameGet:function (state) {
          return state.postName
        }
      },
      //this.$store.commit('post/getName',{postName:"Ultraman"})
      mutations:{
        /**
         * 获取名称
         * @param {*} state 状态
         * @param {object} param 对象参数
         */
        getName:function (state,param) {
          state.postName = "Ace" + param.postName;
        }
      },
      actions:{
        //this.$store.dispatch('post/getNameAsync',{postName:"Ultraman"})
        getNameAsync:function(context,param){
          //在区域内部调用方法，不需要天机命名空间
          //如此是错误的：context.commit('post/getName',param);
          context.commit('getName',param);
        }
      }
    }
  }
})
