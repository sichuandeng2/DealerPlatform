<template>
  <div>
    <div class="cart-list">
      <ul>
        <li v-for="typeName in typeNames" :key="typeName.name">
          <p>
            <i :class="{'cart-select':typeName.isChecked}" @click="selectType(typeName)">√</i>
            <span>{{typeName.name}}</span>
          </p>
          <div v-for="cart in getCartsByTypeName(typeName.name)" :key="cart.cartGuid">
            <i :class="{'cart-select':cart.cartSelected}" @click="selectCart(cart)">√</i>
            <img v-lazy="cart.productPhotoUrl" alt />
            <p class="p-name">{{cart.productName}}</p>
            <p class="p-price">&yen;{{cart.salePrice|price}}</p>
            <p class="p-num">
              <span class="sub-num" @click="sub(cart)">-</span>
              <input type="text" :value="cart.productNum" @change="changeNum(cart)" />
              <span class="add-num" @click="add(cart)">+</span>
              <b>块</b>
            </p>
          </div>
        </li>
      </ul>
    </div>
    <div class="total-pad">
      <i :class="{'cart-select':isAllChecked}" @click="selectAll()">√</i>
      <span>全选</span>
      <span>
        合计：&yen;
        <b>{{totalPrice|price}}</b>
      </span>
      <router-link to="/orderconfirm" tag="button">确定下单</router-link>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      carts: [],
      typeNames: [],
      isAllChecked:false,
      totalPrice:0
    };
  },
  mounted() {
    this.$parent.bottomMenuIndex = 2;
    this.getCarts();
  },
  methods: {
    /**
     * 获取指定用户的购物车
     */
    getCarts: function () {
      //this.$http.defaults.headers.authorization="Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiJBSEFRV1laIiwiZXhwIjoxNTk4ODgzOTgxLCJpc3MiOiJ3ZWJhcGkuY24iLCJhdWQiOiJXZWJBcGkifQ.ChdMeCgNRlUW9_OQgQqEYH1IgsYHr3GW6RUHqkjztrI";
      this.$http
        .get("ShoppingCart", {
          //params: { cid: 1 },
          // headers:{
          //   authorization:"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiJBSEFRV1laIiwiZXhwIjoxNTk4ODgzOTgxLCJpc3MiOiJ3ZWJhcGkuY24iLCJhdWQiOiJXZWJBcGkifQ.ChdMeCgNRlUW9_OQgQqEYH1IgsYHr3GW6RUHqkjztrI"
          // }
        })
        .then((res) => {
          this.carts = res.data.carts;
          this.typeNames = res.data.typeNames;
          // res.data.typeNames.array.forEach(element => {
          //   this.typeNames.push({
          //     name:element,
          //     isChecked:false
          //   })
          // });
          console.log(this.carts);
          console.log(this.typeNames);
        });
    },
    /**
     * 根据物品类型获取购物车（分组时使用）
     */
    getCartsByTypeName: function (typeName) {
      return this.carts.filter((m) => m.typeName == typeName);
    },
    /**
     * 减少购物车
     */
    sub: function (cart) {
      if (cart.productNum >= 0) {
        cart.productNum--;
        this.setCartNum(cart.cartGuid, cart.productNum);
        if (cart.productNum == 0) this.setCartRemove(cart.cartGuid);
      }
    },
    /**
     * 增加购物车
     */
    add: function (cart) {
      cart.productNum++;
      this.setCartNum(cart.cartGuid, cart.productNum);
    },
    /**
     * 改变购物车数量
     */
    changeNum: function (cart) {
      var currNum = parseInt(event.target.value);
      //Na(否)，N(数字) ∴ isNaN(是不是不是数字) ∴ !isNaN(是不是数字)
      if (!isNaN(currNum) && currNum >= 0) {
        if (currNum == 0) {
          //如果currNum为0，说明是删除物品
          this.setCartRemove(cart.cartGuid);
          return;
        }
        cart.productNum = currNum;
        this.setCartNum(cart.cartGuid, cart.productNum);
      } else event.target.value = cart.productNum;
    },
    /**
     * 和后端交互，设置购物车数量
     */
    setCartNum: function (cartGuid, num) {
      var guids = [];
      guids.push(cartGuid);
      this.$http
        .put("ShoppingCart", { cartGuids: guids, buyNum: num })
        .then((res) => {
          if (res.data) this.$msg("物品数量修改成功！");
        });
    },
    /**
     * 选择类型
     */
    selectType: function (typeName) {
      /*
      typeName.isChecked = !typeName.isChecked;
      // if(typeName.isChecked){
      //   this.carts.filter(m=>m.typeName==typeName.name).forEach(element => element.cartSelected = true);
      // }else{
      //   this.carts.filter(m=>m.typeName==typeName.name).forEach(element => element.cartSelected = false);
      // }
      this.carts
        .filter((m) => m.typeName == typeName.name)
        .forEach((element) => (element.cartSelected = typeName.isChecked));
      //准备存放购物车guid的容器
      var cartGuids = [];
      this.carts
        .filter((m) => m.typeName == typeName.name)
        .forEach((m) => {
          cartGuids.push(m.cartGuid);
        });
      //更新数据库
      this.setCartCheck(cartGuids, typeName.isChecked);
      */

      //-----------------------------华丽的分割线-----------------------------
      typeName.isChecked = !typeName.isChecked;
      var cartGuids=[];
      this.carts.filter(m=>m.typeName == typeName.name).forEach(e => {
        if(e.cartSelected != typeName.isChecked){
          cartGuids.push(e.cartGuid);
          e.cartSelected = typeName.isChecked;
        }
      });
      //更新数据库
      this.setCartCheck(cartGuids, typeName.isChecked);
    },
    selectCart:function(cart){
      cart.cartSelected = !cart.cartSelected;
      var cartGuids = [];
      cartGuids.push(cart.cartGuid);
       //更新数据库
      this.setCartCheck(cartGuids, cart.cartSelected);
    },
    /**
     * 与后端交互，设置购物车的选中专状态
     */
    setCartCheck: function (cartGuids, isChecked) {
      this.$http.put("shoppingCart",{cartCheck:isChecked,cartGuids}).then(res=>{
        if(!res.data)this.$msg("购物车选中出错");
      });
    },
    /**
     * 与后端交互，删除购物车
     */
    setCartRemove: function (cartGuid) {
      this.$alert.confirm("是否删除购物车").then((action) => {
        this.$http
          .delete("ShoppingCart", {
            params: {
              cartGuid,
            },
          })
          .then((res) => {
            if (res.data) {
              history.go(0);

              // 方法一
              // var index =this.carts.findIndex(m=>m.cartGuid == cartGuid);
              // this.carts.splice(index,1);
              // 方法二
              // for (let i = 0; i < this.carts.length; i++) {
              //   const element = array[i];
              //   if(element.cartGuid == cartGuid){
              //     this.carts.splice(i,1);
              //     }
              // }
            }
          });
      });
    },
    /**
     * 全选商品
     */
    selectAll:function(){
      //准备一个容器，用于存放改变的购物车的guid
      var cartGuids = [];
      this.isAllChecked = !this.isAllChecked;
      this.carts.forEach(e=>{
       if(e.cartSelected != this.isAllChecked){
          cartGuids.push(e.cartGuid);
          e.cartSelected = this.isAllChecked;
        }
      })
      this.setCartCheck(cartGuids,this.isAllChecked);
    }
  },
  computed: {},
  watch: {
    /**
     * 被监听的对象
     */
    carts:{
      /**
       * 监听发生变化后的执行动作
       */
      handler(nVal,oVal){
        //判定是否选中大类
        //循环大类
        for (let i = 0; i < this.typeNames.length; i++) {
          var typeName = this.typeNames[i];
          //获取当前大类的购物车
          var carts= this.carts.filter(m=>m.typeName == typeName.name);
          /*
          //方法一
          var allNum = carts.length;//获取当前类型下所有的购物车
          var selNum = carts.filter(m=>m.cartSelected).length;//获取当前所有选中的购物车
          //两厢比较，如果数量一致则说明全选了 
          typeName.isChecked = allNum == selNum;
          */

          /**
           * 方法二，是否每一个都是选中状态 
           * 即确定的序列中是否所有的元素都满足条件
           * 类比C#中的All()方法
           */
          //typeName.isChecked = carts.every(m=>m.cartSelected);

          /**
           * 方法三，是否存在未被选中的购物车
           * 即确定的序列中是否存在满足指定条件的元素
           * 类比C#中的Any()方法
           */
          typeName.isChecked = !carts.some(m=>!m.cartSelected);
        }
        //跟据大类即typeName来判断全选按钮是否被选中
        var allTypeNum = this.typeNames.length;
        var selTypeNum = this.typeNames.filter(m=>m.isChecked).length;
        // if(allTypeNum == selTypeNum) this.isAllChecked = true;
        // else this.isAllChecked = false;
         this.isAllChecked = allTypeNum == selTypeNum;

         //计算商品总价
         this.totalPrice = 0;
         var selCart = this.carts.filter(m=>m.cartSelected).forEach(m=>{
           this.totalPrice += m.salePrice*m.productNum;
         })
         //this.totalPrice = this.totalPrice.toFixed(2);
      },
      /**
       * 执行深度监听
       */
      deep:true

    }
  },
};
</script>

<style lang="scss" scoped>
i {
  position: absolute;
  width: 22px;
  height: 22px;
  border: 1px solid #a9a9a9;
  border-radius: 22px;
  left: 10px;
  top: 13px;
  color: white;
  text-align: center;
  font-style: normal;
  font-size: 12px;
  line-height: 22px;
  font-weight: bold;
  background-color: #fff;
}
i.cart-select {
  background-color: crimson;
  border: 1px solid crimson;
  color: white;
}
.cart-list {
  ul {
    li {
      background-color: #fff;
      margin-bottom: 10px;
      text-align: left;

      > p {
        position: relative;
        height: 45px;
        border-bottom: 1px solid #ddd;
        padding-left: 40px;

        span {
          display: inline-block;
          border-left: 3px solid crimson;
          height: 28px;
          margin-top: 10px;
          padding-left: 8px;
          line-height: 28px;
        }
      }
      div {
        padding: 8px 10px 8px 130px;
        position: relative;
        height: 98px;
        i {
          top: 30px;
        }
        img {
          width: 66px;
          height: 66px;
          position: absolute;
          top: 12px;
          left: 50px;
        }
        p.p-name {
          font-size: 12px;
          height: 30px;
          margin-top: 5px;
        }
        p.p-price {
          font-size: 13px;
          height: 20px;
          color: crimson;
        }
        p.p-num {
          text-align: right;
          padding-right: 20px;
          span {
            display: inline-block;
            width: 22px;
            height: 22px;
            border-radius: 11px;
            border: 1px solid rgb(107, 107, 107);
            text-align: center;
            line-height: 20px;
          }
          input {
            width: 25px;
            border: none 0px;
            outline: none;
            text-align: center;
          }

          b {
            font-weight: normal;
            margin-left: 10px;
            font-size: 13px;
          }
        }
      }
    }
  }
}
.total-pad {
  height: 60px;
  width: 100%;
  background-color: #383838;
  position: fixed;
  left: 0;
  bottom: 40px;
  text-align: left;
  padding-left: 50px;
  line-height: 60px;
  i {
    top: 20px;
  }
  span {
    color: #fff;
    margin-left: 6px;
    font-size: 13px;
    b {
      font-size: 15px;
    }
  }
  button {
    float: right;
    height: 60px;
    width: 120px;
    background-color: #ddd;
    color: #aaa;
    border: 0 none;
    font-size: 15px;
    font-weight: bold;
  }
}
</style>