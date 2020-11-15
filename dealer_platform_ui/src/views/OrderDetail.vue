<template>
  <div v-if="orderInfo.customerInvoices !=null">
    <backhead text="订单详情" />
    <div class="order-info" >
      <p>
        <img src="/img/icons/logo3.png" alt />
        <b>订单编号：{{ orderInfo.saleOrderNo }}</b>
      </p>
      <ul>
        <li v-for="orderDetail in orderInfo.orderDetails" :key="orderDetail.id">
          <img :src="orderDetail.productPhotoUrl" alt />
          <p class="p-name">{{ orderDetail.productName }}</p>
          <p class="p-price">
            &yen;{{ orderDetail.salePrice | price }}
            <i>x{{ orderDetail.orderNum }}</i>
          </p>
        </li>
      </ul>
      <!-- 完成订单步骤图 -->
      <div :class="['order-progress', { 'order-progress-down': progressShow }]">
        <div class="progress-node">
          <el-steps
            :space="200"
            :active="getStepSn()"
            finish-status="success"
          >
            <el-step title="已下单"></el-step>
            <el-step title="已审批"></el-step>
            <el-step title="已发货"></el-step>
            <el-step title="已收货"></el-step>
          </el-steps>
        </div>
        <div class="show-progress" @click="showProgress">
          <img src="/img/dealerImgs/down.png" alt />
        </div>
      </div>
    </div>
    <div class="order-set">
      <p class="o-item">
        <span>备注</span>
        <span>{{ orderInfo.remark | remark }}</span>
      </p>
      <p class="o-item">
        <span>交货日期</span>
        <span>{{ orderInfo.deliveryDate | date }}</span>
      </p>
      <p class="o-submit">
        <button class="cancel" @click="cancelOrder">取消订单</button>
        <button class="again" @click="buyAgain">再次购买</button>
      </p>
      <p class="o-item">
        <span>开票人</span>
        <span>{{ orderInfo.customerInvoices.invoiceNo }}</span>
      </p>
      <p class="o-item">
        <span>开户行</span>
        <span>{{ orderInfo.customerInvoices.invoiceBank }}</span>
      </p>
    </div>
  </div>
</template>

<script>
import backhead from "../components/BackHead";
export default {
  data() {
    return {
      orderNo: "",
      progressShow: false,
      orderInfo: {},
    };
  },
  async created() {
    this.orderNo = this.$route.params.orderno;
    var res = await this.getOrderInfo();
    this.orderInfo = res.data;
    console.log(this.orderInfo);
  },
  mounted() {
  },
  methods: {
    showProgress: function () {
      this.progressShow = !this.progressShow;
    },
    getOrderInfo: async function () {
      var res = await this.$http.get(`orderinfo/${this.orderNo}`);
      return res;
    },
    cancelOrder: function () {},
    buyAgain: function () {},
    getStepSn:function(){
      if(this.orderInfo.orderProgresses==null){
        return 1;
      }
      else{
        return this.orderInfo.orderProgresses.stepSn
      }
    }
  },
  components: {
    backhead,
  },
};
</script>

<style lang="scss" scoped>
.order-info {
  background-color: #fff;
  > p {
    text-align-last: left;
    height: 50px;
    line-height: 50px;
    padding: 0 10px;
    border-bottom: 1px solid #ddd;
    position: relative;
    padding-left: 30px;
    img {
      width: 25px;
      height: 25px;
      vertical-align: middle;
      position: absolute;
      top: 11px;
      left: 10px;
    }
    b {
      display: inline-block;
      overflow: hidden;
      font-size: 14px;
      margin-left: 10px;
      color: #333;
      text-overflow: ellipsis;
      white-space: nowrap;
      width: 300px;
    }
  }
  ul {
    padding-bottom: 6px;
    background-color: #fff;
    background-image: url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAIAAAAAKBAMAAACOO0tGAAAABGdBTUEAALGPC/xhBQAAAAFzUkdCAK7OHOkAAAAnUExURf///4u16Oxtbezz/J3B7NDh9vvd3fSmprjS8vGNje57e/jDw/7w8IQ3dnAAAABSSURBVCjPY2CAAG4l7GADVJ4hUBArEIXJJ2HXrw6TZ8auX7ABKs+zCLsBE2AGmGDXLwKT58SuX+sATIEjdgOcYfJF2A1Qg8lz4PCBwWgQUiMIAWCaOFG2MdFRAAAAAElFTkSuQmCC);
    background-repeat: repeat-x;
    background-position: bottom;
    li {
      padding: 10px 15px 10px 100px;
      background-color: #fff;
      position: relative;
      border-bottom: 1px solid #ddd;
      img {
        position: absolute;
        width: 66px;
        height: 66px;
        left: 10px;
        top: 10px;
      }
      p {
        text-align: left;
        height: 25px;
        line-height: 25px;
      }
      .p-name {
        font-size: 13px;
        color: #333;
        font-weight: bold;
        height: 50px;
      }
      // .p-remark {
      //   color: #666;
      //   font-size: 12px;
      //   input {
      //     color: #666;
      //     font-size: 12px;
      //     border: 0 none;
      //     outline: none;
      //   }
      // }
      .p-price {
        color: red;
        font-size: 14px;
        font-weight: bold;
        i {
          font-style: normal;
          color: #666;
          font-weight: normal;
          position: absolute;
          right: 66px;
          font-size: 12px;
        }
      }
    }
  }
  .order-progress {
    padding: 0 10px;
    height: 16px;
    text-align: center;
    transition: 500ms;
    position: relative;
    overflow: hidden;
    .progress-node {
      width: 80%;
      margin: auto;
      margin-top: 16px;
    }
    .show-progress {
      position: absolute;
      height: 16px;
      width: 100%;
      left: 0;
      bottom: 0;
      text-align: center;
      img {
        background-color: transparent;
        width: 20px;
        height: 6px;
        margin-bottom: 6px;
        transition: 0.5s;
      }
    }
  }
  .order-progress-down {
    height: 96px;
    img {
      transform: rotate(180deg);
    }
  }
}
.order-set {
  background-color: #fff;
  text-align: left;
  p {
    margin: 0 10px;
    border-bottom: 1px solid #ddd;
  }
  .o-item {
    height: 40px;
    font-size: 13px;
    line-height: 40px;
    span {
      margin-right: 10px;
      color: #666;
    }
    select,
    input {
      color: #666;
      font-size: 12px;
      border: 0 none;
      outline: none;
      width: 160px;
    }
    input[type="date"] {
      padding-left: 3px;
      background: #ddd;
      height: 26px;
      border-radius: 3px;
    }
  }
  .o-noti {
    font-size: 13px;
    color: #666;
    font-weight: bold;
    padding: 6px 0;
    margin-bottom: 5px;
    span {
      display: block;
      b {
        color: crimson;
      }
    }
    span:last-child {
      margin-top: 6px;
      font-size: 12px;
      font-weight: normal;
    }
  }
  .o-submit {
    display: flex;
    border: 0px solid #ddd;
    button {
      flex: 1;
      width: 100%;
      height: 40px;

      color: #fff;
      border-radius: 3px;
      margin: auto;
      border: 0 none;
      font-size: 15px;
      font-weight: bold;
    }
    button.cancel {
      background: rgb(168, 168, 168);
      margin-right: 5px;
    }
    button.again {
      margin-left: 5px;
      background: rgb(255, 136, 0);
    }
  }
}
</style>
<style lang="scss">
.el-step {
  position: relative;
  .el-step__icon {
    display: block !important;
  }
  .el-step__title {
    font-size: 14px !important;
    font-weight: normal;
    position: relative;
    left: -10px;
  }
}
.el-step:last-child {
  max-width: 5.3% !important;
  .el-step__main {
    position: relative;
    left: -20px;
    width: 50px;
  }
}
</style>