<template>
  <div>
    <!--展示子页面内容-->
    <router-view />
    <!--展示底部三个菜单-->
    <div class="foot-menu-pad">
      <div class="foot-menu">
        <router-link to="/productList" class="foot-item">
          <b :class="['i-search',{'menu-sel':bottomMenuIndex==1}]"></b>
        </router-link>
        <router-link to="/shoppingCart" class="foot-item">
          <b :class="['i-cart',{'menu-sel':bottomMenuIndex==2}]">
            <!-- <i>{{$store.state.cartNum}}</i> -->
            <i>{{$store.getters.getCartNum}}</i>
          </b>
        </router-link>
        <router-link to="/main" class="foot-item">
          <b :class="['i-user',{'menu-sel':bottomMenuIndex==3}]"></b>
        </router-link>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      bottomMenuIndex: 3,
    };
  },
  mounted() {
   this.$store.dispatch('getCartNumAsync');
   this.$store.dispatch('getUnFinishedOrderNumAsync');
  },
  methods: {
  },
  // computed: {
  //   getNum:()=>()=>1
  // },
};
</script>
<style>
.icon-success {
  background-image: url("/img/icons/Logo2.png");
  background-size: 60px;
  height: 60px;
  width: 60px;
  margin: auto;
  background-repeat: no-repeat;
}
</style>
<style lang="scss" scoped>
.foot-menu-pad {
  height: 40px;

  .foot-menu {
    height: 40px;
    position: fixed;
    background-color: #fff;
    width: 100%;
    left: 0;
    bottom: 0;
    display: flex;

    .foot-item {
      flex: 1;
      height: 40px;
      line-height: 40px;

      b {
        background-color: #acacac;
        width: 26px;
        height: 26px;
        border-radius: 13px;
        display: inline-block;
        margin-top: 6px;
        background-position: center;
        background-repeat: no-repeat;

        i {
          position: relative;
          font-size: 12px;
          color: #fff;
          background-color: red;
          padding: 1px 3px;
          font-weight: normal;
          border-radius: 12px;
          top: -18px;
          left: 15px;
          font-style: normal;
          width: 22px;
          display: inline-block;
          height: 22px;
          line-height: 21px;
        }
      }

      .i-search {
        background-image: url("/img/icons-png/search-white.png");
      }

      .i-cart {
        background-image: url("/img/icons-png/shoppingCar-white.png");
      }

      .i-user {
        background-image: url("/img/icons-png/user-white.png");
      }
      .menu-sel {
        background-color: rgb(201, 39, 39);
      }
    }
  }
}
</style>
