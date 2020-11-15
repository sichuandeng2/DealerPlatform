<template>
  <div>
    <backhead text="订单确认"></backhead>
    <div class="order-info">
      <p>
        <img src="/img/icons/logo3.png" alt />
        <b>订单信息</b>
      </p>
      <ul>
        <li v-for="order in confirmOrders" :key="order.id">
          <img :src="order.productPhotoUrl" alt />
          <p class="p-name">{{order.productName}}</p>
          <p class="p-price">
            &yen;{{order.salePrice}}
            <i>x{{order.productNum}}</i>
          </p>
        </li>
      </ul>
    </div>
    <div class="order-set">
      <p class="o-item">
        <span>备注</span>
        <input type="text" placeholder="选填：对本次交易的说明" v-model="remark"/>
      </p>
      <p class="o-item">
        <span>交货日期</span>
        <input type="date" name id v-model="deliveryDate" />
      </p>
      <p class="o-item">
        <span>开票人</span>
        <select v-model="selInvoice">
          <option
            v-for="invoice in invoices"
            :key="invoice.id"
            :value="invoice.id"
          >{{invoice.invoiceNo}}</option>
        </select>
      </p>
      <p class="o-noti">
        <span>
          共{{getTotalNum}}件，合计：
          <b>&yen;{{getTotalPrice|price}}</b>
        </span>
        <span>注：显示金额为成本金额，不含运费，实际结算价格以审单后为准</span>
      </p>
      <p class="o-submit">
        <button @click="addOrder">提交订单</button>
      </p>
    </div>
  </div>
</template>

<script>
import backhead from '../components/BackHead';
export default {
  data() {
    return {
      confirmOrders: [],
      invoices: [],
      selInvoice: null,
      deliveryDate: "",
      remark:""
    };
  },
  mounted() {
    this.getConfirmOrder();
  },
  methods: {
    getConfirmOrder: function () {
      this.$http.get("OrderConfirm").then((res) => {
        console.log(res.data);
        this.confirmOrders = res.data.orders;
        this.invoices = res.data.invoices;
        this.selInvoice = res.data.invoices[0].id;
        this.deliveryDate = this.getDeliveryDate;
      });
    },
    addOrder:function(){
      console.log(this.selInvoice);
      this.$http.post("OrderConfirm",{
        remark:this.remark,
        deliveryDate:this.deliveryDate,
        invoiceNo:this.selInvoice+""
        }).then(res=>{
       console.log(res);
       if(res.status ==200 && res.data.split("|")[0] == "success"){
          var orderNo = res.data.split("|")[1];
          this.$router.push({name:"OrderSuccess",params:{orderno:orderNo}});
        }
      })
    }
  },
  computed: {
    getTotalPrice: function () {
      var totalPrice = 0;
      this.confirmOrders.forEach((e) => {
        totalPrice += e.salePrice * e.productNum;
      });
      return totalPrice;
    },
    getTotalNum:function(){
  var totalNum = 0;
      this.confirmOrders.forEach((e) => {
        totalNum +=  e.productNum;
      });
      return totalNum;
    },
    /**
     * 交期//不是这个娇妻
     */
    getDeliveryDate: function () {
      var date = new Date();
      var year = date.getFullYear();
      var month =
        date.getMonth() + 1 < 10
          ? "0" + (date.getMonth() + 1)
          : date.getMonth() + 1;
      var day =
        date.getDate() + 1 < 10
          ? "0" + (date.getDate() + 1)
          : date.getDate() + 1;
      return `${year}-${month}-${day}`;
      //dateTime.now().toString(yyyy-MM-dd);
    },
  },
  components:{
    backhead
  }
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
    img {
      width: 25px;
      height: 25px;
      vertical-align: middle;
    }
    b {
      font-size: 14px;
      margin-left: 10px;
      color: #333;
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
    button {
      width: 100%;
      height: 40px;
      background: crimson;
      color: #fff;
      border-radius: 3px;
      margin: auto;
      border: 0 none;
      font-size: 15px;
      font-weight: bold;
    }
  }
}
</style>