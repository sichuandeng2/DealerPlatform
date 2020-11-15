import Vue from 'vue'
import VueRouter from 'vue-router'
import Login from '../views/Login.vue'
import Layout from '../views/Layout.vue'
import Main from '../views/Main.vue'
import ProductList from '../views/ProductList.vue'
import ShoppingCart from '../views/ShoppingCart.vue'
import OrderConfirm from '../views/OrderConfirm.vue'
import OrderSuccess from '../views/OrderSuccess.vue'
import OrderDetail from '../views/OrderDetail.vue'
import OrderList from '../views/OrderList.vue'

Vue.use(VueRouter)

  const routes = [
  {
    path: '/',
    name: 'login',
    component: Login
  },
  {
    path: '/layout',
    name: 'layout',
    component: Layout,
    children:[
      {
        path:'/main',
        name:'main',
        component:Main
      },
      {
        path:'/productList',
        name:'productList',
        component:ProductList
      },
      {
        path:'/shoppingCart',
        name:'shoppingCart',
        component:ShoppingCart
      },
      {
        path:'/orderConfirm',
        name:'orderConfirm',
        component:OrderConfirm
      },
      {
        path:'/orderSuccess/:orderno',
        name:'OrderSuccess',
        component:OrderSuccess
      },
      {
        path:'/orderDetail/:orderno',
        name:'OrderDetail',
        component:OrderDetail
      },
      {
        path:'/orderList',
        name:'OrderList',
        component:OrderList
      }
    ]
  }
]

const router = new VueRouter({
  routes
})

export default router
