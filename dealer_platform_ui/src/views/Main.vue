<template>
  <div>
    <div class="user-box">
      <div class="user-info">
        <div class="user-head">
          <img src="/img/dealerImgs/picUser.png" alt />
        </div>
        <p class="user-name" @click="setCart">ACE</p>
        <p>销售员：菜菜</p>
        <p>单位地址：江苏镇江 万达广场A座</p>
      </div>
    </div>
    <diV class="menu-item">
      <img src="/img/dealerImgs/purchase_order.png" alt />
      <router-link to="/orderList" class="menu-info">
        <p class="m-title">我的订单</p>
        <p class="m-info">未完成的订单：{{$store.getters.getUnFinedNum}}个</p>
      </router-link>
    </diV>
    <diV class="menu-item">
      <img src="/img/dealerImgs/shopping212.png" alt />
      <router-link to="/shoppingcart" class="menu-info">
        <p class="m-title">购物车</p>
        <p class="m-info">购物车中有：{{$store.getters.getCartNum}}个待下单的物品</p>
      </router-link>
    </diV>
    <diV class="menu-item">
      <img src="/img/dealerImgs/door.png" alt />
      <router-link to="" class="menu-info">
        <p class="m-title">退出账号</p>
        <p class="m-info">退出当前账号</p>
      </router-link>
    </diV>
  </div>
</template>

<script>
export default {
  mounted() {
    this.$parent.bottomMenuIndex = 3;
    // this.$store.dispatch('getNameAsync',{firstName:"ultraman"})
    // alert(this.$store.getters.nameGet);
    console.log( this.$store.dispatch('post/getNameAsync',{postName:"Ultraman"}));
    console.log( this.$store.getters["post/nameGet"])
  },
  methods: {
      setCart:function(test){
           this.$store.commit("setCartNum",8);
      }
  },
};
</script>

<style lang="scss" scoped>
body {
  background-color: #ccc;
}
.user-box {
  padding: 10px;
  .user-info {
    padding: 15px 0 25px 80px;
    background: -webkit-linear-gradient(left, #b70101, #f20505);
    height: 100px;
    border-radius: 10px;
    position: relative;
    p {
      text-align: left;
      color: #fff;
      font-size: 14px;
      margin-bottom: 5px;
    }
    .user-head {
      height: 40px;
      width: 40px;
      border-radius: 20px;
      overflow: hidden;
      position: absolute;
      top: 20px;
      left: 25px;
      img {
        width: 40px;
        height: 40px;
      }
    }
  }
}
.menu-item {
  height: 60px;
  background-color: #fff;
  margin-top: 10px;
  padding: 10px 0px 0px 60px;
  position: relative;
  img {
    position: absolute;
    top: 16px;
    left: 20px;
  }
  .menu-info {
      color:#666;
      text-decoration: none;
      text-align: left;
    .m-title {
      font-weight: bold;
      font-size: 15px;
    }
    .m-info {
      font-size: 14px;
    }
  }
}
</style>