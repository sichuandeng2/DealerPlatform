<template>
  <div>
    <backhead text="订单列表"></backhead>
    <div class="order-list">
      <ul>
        <router-link :to="'/orderDetail/'+orderMaster.saleOrderNo" tag="li" v-for="orderMaster in orderMasters" :key="orderMaster.saleOrderNo">
          <p class="order-no">订单号{{ orderMaster.saleOrderNo }}</p>
          <hr />
          <div class="order-info">
            <img
              alt=""
              :src="orderDetail.productPhotoUrl"
              v-for="orderDetail in orderMaster.orderDetails"
              :key="orderDetail.id"
            />
            <p v-if="orderMaster.orderDetails.length == 1" class="order-name">
              {{orderMaster.orderDetails[0].productName}}
            </p>
          </div>
          <p class="order-price">
            <span
              >有{{ orderMaster.orderDetails.length }}样商品，共{{
                getTotalCount(orderMaster.orderDetails)
              }}件</span
            >
            <b
              >总金额：&yen;{{
                transPrice(getTotalPrice(orderMaster.orderDetails))
              }}</b
            >
          </p>
        </router-link>
      </ul>
    </div>
  </div>
</template>

<script>
import backhead from "../components/BackHead";
export default {
  data() {
    return {
      orderMasters: [],
    };
  },
  mounted() {
    this.getOrderList();
  },
  methods: {
    getOrderList: function () {
      this.$http.get("orderinfo").then((res) => {
        this.orderMasters = res.data;
      });
    },
    getTotalCount: function (details) {
      var totalCount = 0;
      details.forEach((e) => {
        totalCount += e.orderNum;
      });
      return totalCount;
    },
    getTotalPrice: function (details) {
      var totalPrice = 0;
      details.forEach((e) => {
        totalPrice += e.orderNum * e.salePrice;
      });
      return totalPrice;
    },
    transPrice: (price) => price.toFixed(2),
  },
  components: {
    backhead,
  },
};
</script>

 <style lang="scss" scoped>
.order-list {
  ul {
    li {
      list-style: none;
      background-color: #fff;
      margin-top: 10px;
      padding: 10px;

      .order-no {
        border-left: 3px solid crimson;
        padding-left: 6px;
        font-size: 13px;
        text-align-last: left;
      }

      hr {
        color: #ddd;
        margin: 10px 0;
      }

      .order-info {
        height: 72px;
        position: relative;
        padding: 6px 0;
        overflow-x: auto;
        display: flex;
        img {
          flex-shrink: 0;
          margin-right: 10px;
          width: 60px;
          height: 60px;
          background-color: #ccc;
        }

        .order-name {
          float: left;
          font-size: 13px;
          font-weight: bold;
          color: #666;
          height: 45px;
        }
      }

      .order-price {
        overflow: hidden;

        span {
          font-size: 13px;
          color: #666;
          float: left;
        }

        b {
          color: crimson;
          font-size: 14px;
          float: right;
        }
      }
    }
  }
}
</style>
    