<template>
  <div>
    <div class="search-pad">
      <input type="text" v-model="searchText" />
      <button @click="showRight">筛选</button>
    </div>
    <div class="system-pad">
      <div
        :class="['system-item',{'system-select':systemBloneType == 'BC'}]"
        @click="selectBloneType('BC')"
      >
        <span>板材</span>
      </div>
      <div
        :class="['system-item',{'system-select':systemBloneType == 'WJ'}]"
        @click="selectBloneType('WJ')"
      >
        <span>五金</span>
      </div>
      <div
        :class="['system-item',{'system-select':systemBloneType == 'DB'}]"
        @click="selectBloneType('DB')"
      >
        <span>地板</span>
      </div>
    </div>
    <div class="product-list">
      <ul>
        <li v-for="product in products" :key="product.id">
          <img v-lazy="product.productPhoto.productPhotoUrl" alt />
          <div>
            <p class="p-name">{{product.productName}}</p>
            <p class="p-type">类别：{{product.typeName}}</p>
            <p class="p-price">&yen;{{product.productSale.salePrice}}元/张</p>
            <span class="p-add-cart" @click="addCart(product.productNo)"></span>
          </div>
        </li>
      </ul>
      <div :class="['left-menu',{'left-menu-show':isShowLeft}]">
        <div class="left-switch" @click="showLeft">
          <img src="/img/dealerImgs/up.png" alt />
        </div>
        <ul>
          <li
            v-for="typeName in typeNames"
            :key="typeName"
            :class="{'left-item-select':leftSelectName == typeName}"
            @click="selectLeft(typeName)"
          >{{typeName}}</li>
        </ul>
      </div>
    </div>
    <div class="right-pad">
      <ul class="f-type-list">
        <li v-for="productType in productTypes" :key="productType.typeName">
          <p @click="showTypeAll(productType.typeItems,productType.typeName)">
            {{productType.typeNameCn}}
            <img
              :class="{'show-type-all':isShowTypeAll==productType.typeName}"
              src="/img/dealerImgs/up.png"
              alt
              v-if="productType.typeItems.length>6"
            />
          </p>
          <ul :class="['f-item-list',{'show-type-all':isShowTypeAll==productType.typeName}] ">
            <li
              v-for="typeItem in productType.typeItems"
              :key="typeItem"
              @click="selectProductType(productType.typeName,typeItem)"
            >
              <span
                :class="{'prop-select':productTypeSelected[productType.typeName] == typeItem}"
              >{{typeItem}}</span>
            </li>
          </ul>
        </li>
      </ul>
      <div class="right-edit">
        <button style="background-color:rgb(188,0,0);color:#fff" @click="confirmFilter">确定</button>
        <button @click="hideRight">取消</button>
      </div>
    </div>
    <div class="cover" v-show="isShowCover" @click="hideRight"></div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      systemBloneType: "BC",
      pageSize: 10,
      pageIndex: 1,
      products: [],
      searchText: "",
      isShowLeft: false,
      typeNames: [],
      leftSelectName: null,
      searchTimeout: null,
      productTypes: [],
      isShowTypeAll: "",
      productTypeSelected: {},
      isShowCover: false,
      transProductTypeSelected: [],
    };
  },
  mounted() {
    this.$parent.bottomMenuIndex = 1;
    this.getProducts();
    window.addEventListener("scroll", this.handleScroll);
  },
  methods: {
    handleScroll: function () {
      //获取整个页面的长度
      var htmlHeight = document.querySelector("html").offsetHeight;
      //获取滚动条距离顶部的长度
      var scrollTop = document.querySelector("html").scrollTop;
      //获取屏幕可视区域的高度
      var screenHeight = document.documentElement.clientHeight;
      //获取 整个页面长度 和 滚动条距离顶部长度的差值
      var diffHeight = htmlHeight - scrollTop;
      //如果 差值小于 屏幕可视区域高度+2 ，并且 滚动条距离顶部高度大于0
      if (diffHeight <= screenHeight + 2 && scrollTop > 0) {
        this.pageIndex++;
        this.getProducts();
      }
    },
    getProductTypes: function (bType, typeName) {
      this.$http
        .get(`product/${bType}/${typeName}/getProductTypes`)
        .then((res) => {
          this.productTypes = res.data;
        });
    },
    selectBloneType: function (bType) {
      if (bType != this.systemBloneType) {
        this.pageIndex = 1;
        this.systemBloneType = bType;
        this.leftSelectName = null;
        this.getProducts();
      }
    },
    getTypeName: function (bType) {
      this.$http.get(`product/${bType}/GetTypeName`).then((res) => {
        this.typeNames = res.data;
      });
    },
    getProducts: function () {
      var bType = this.systemBloneType;
      var typeName = this.leftSelectName == null ? "空" : this.leftSelectName;
      var pageSize = this.pageSize;
      var pageIndex = this.pageIndex;
      var searchText = this.searchText;
      var productTypes = this.transProductTypeSelected.join("%5E");
      if (pageIndex == 1) {
        this.products = [];
        this.getTypeName(bType);
        this.getProductTypes(bType, typeName);
      }

      this.$http
        .get("product", {
          params: {
            bType,
            typeName,
            pageSize,
            pageIndex,
            searchText,
            productTypes,
          },
        })
        .then((res) => {
          res.data.forEach((m) => {
            this.products.push(m);
          });
        });
    },
    showLeft: function () {
      this.isShowLeft = !this.isShowLeft;
    },
    selectLeft: function (typeName) {
      if (this.leftSelectName != typeName) {
        this.productTypeSelected = {};
        this.leftSelectName = typeName;
      } else {
        this.leftSelectName = null;
      }
      this.pageIndex = 1;
      this.getProducts();
    },
    showTypeAll: function (items, key) {
      if (items.length > 6) {
        if (this.isShowTypeAll == key) this.isShowTypeAll = "";
        else this.isShowTypeAll = key;
      }
    },
    selectProductType: function (typeName, typeItem) {
      if (this.productTypeSelected[typeName] == typeItem)
        //直接删除属性
        delete this.productTypeSelected[typeName];
      else this.productTypeSelected[typeName] = typeItem;
      this.$forceUpdate();
    },
    getSelectProductType: function () {
      this.transProductTypeSelected = [];
      for (const key in this.productTypeSelected) {
        if (this.productTypeSelected.hasOwnProperty(key)) {
          const typeItem = this.productTypeSelected[key];
          const typeName = key;
          this.transProductTypeSelected.push(typeName + "_" + typeItem);
        }
      }
    },
    confirmFilter: function () {
      this.getSelectProductType();
      this.pageIndex = 1;
      this.getProducts();
      this.hideRight();
    },
    showRight: function () {
      this.isShowCover = true;
      document.querySelector(".right-pad").style.right = 0;
    },
    hideRight: function () {
      this.isShowCover = false;
      document.querySelector(".right-pad").style.right = "-85%";
    },
    addCart: function (productNo) {
      productNo = productNo.trim();
      this.$http
        .post("ShoppingCart", {
          customerNo: localStorage["CustomerNo"],
          productNo,
          buyNum: 1,
        })
        .then((res) => {
          if (res.status == 200) {
            if(res.data=="i"){
              //this.$store.commit("addCartNum",1);
              this.$store.dispatch('addCartNumAsync',1);
            }

            this.$msg({
              message: "购物车添加成功！",
              iconClass: "icon icon-success",
              duration: 500,
            });
          } else if (res.status == 204) {
            this.$msg({
              message: "购物车添加失败！",
              duration: 500,
            });
          }
        });
    },
  },
  watch: {
    searchText(nVal, oVal) {
      clearTimeout(this.searchTimeout);
      this.searchTimeout = setTimeout(() => {
        this.pageIndex = 1;
        this.getProducts();
      }, 3000);
    },
  },
};
</script>
<style>

</style>
<style lang="scss" scoped>

.search-pad {
  padding: 6px 20px;
  background-color: #f0f0f0;
  display: flex;
  position: fixed;
  width: 100%;
  z-index: 5;

  input {
    height: 28px;
    border: 1px solid #ddd;
    border-radius: 3px;
    flex: 1;
    outline: none;
    padding: 0 6px;
  }

  button {
    background-color: transparent;
    border: 0 none;
    width: 56px;
    font-size: 14px;
    font-weight: bold;
    outline: none;
  }
}
.system-pad {
  background-color: #fff;
  display: flex;
  position: fixed;
  width: 100%;
  z-index: 5;
  top: 40px;
  .system-item {
    flex: 1;
    text-align: center;
    border-bottom: 1px solid #ddd;
    border-right: 1px transparent solid;
    border-left: 1px transparent solid;
    span {
      border: 0 none;
      background-color: #e4e3e3;
      margin: 6px 5px;
      font-size: 12px;
      padding: 6px 0;
      display: block;
      height: 26px;
      border-radius: 4px;
    }
  }
  .system-select {
    border-bottom: 1px solid transparent;
    border-right: 1px #ddd solid;
    border-left: 1px #ddd solid;
    span {
      background-color: transparent;
    }
  }
}
.product-list {
  padding-top: 79px;
  ul {
    background-color: #fff;
    li {
      list-style: none;
      height: 86px;
      position: relative;
      padding-left: 110px;
      img {
        height: 66px;
        width: 66px;
        background-color: #ccc;
        position: absolute;
        left: 28px;
        top: 10px;
      }
      div {
        padding: 10px 0;
        border-bottom: 1px solid #f0f0f0;
        text-align: left;
        position: relative;
        p {
          font-size: 13px;
        }
        .p-type {
          font-size: 12px;
          margin-top: 8px;
        }
        .p-price {
          margin-top: 8px;
          color: red;
        }
        .p-add-cart {
          display: inline-block;
          width: 38px;
          height: 20px;
          background-color: crimson;
          position: absolute;
          right: 10px;
          bottom: 10px;
          border-radius: 20px;
          background-image: url("/img/dealerImgs/shopping212white.png");
          background-repeat: no-repeat;
          background-size: 20px;
          background-position: center;
        }
      }
    }
  }
  .left-menu {
    position: fixed;
    height: 100%;
    left: -105px;
    width: 125px;
    background-color: #fff;
    top: 79px;
    border-radius: 0 18px 0 0;
    border: 1px solid #d7d7d7;
    overflow: hidden;
    transition: 0.5s;
    .left-switch {
      width: 20px;
      position: absolute;
      right: 0;
      height: 100%;
      img {
        width: 20px;
        position: absolute;
        top: 40%;
        transform: rotate(90deg);
        right: 0px;
        transition: 0.5s;
      }
    }
    ul {
      position: absolute;
      height: 100%;
      width: 105px;
      background-color: #f0f0f0;
      overflow: auto;
      li {
        width: 105px;
        height: 50px;
        text-align: center;
        line-height: 50px;
        border-bottom: 1px solid #d7d7d7;
        font-size: 12px;
        color: #333;
        padding: 0;
      }
      li.left-item-select {
        background-color: #fff;
      }
    }
  }
  .left-menu-show {
    left: 0;
    .left-switch {
      img {
        transform: rotate(-90deg);
      }
    }
  }
}
.right-pad {
  position: fixed;
  right: -85%;
  top: 0;
  width: 85%;
  height: 100%;
  background-color: #f7f7f7;
  z-index: 15;
  transform: 580ms;
  transition: 0.5s;
  ul {
    list-style: none;
  }

  .f-type-list {
    overflow: auto;
    height: 100%;
    width: 100%;
    position: absolute;
    padding-bottom: 40px;
    > li {
      padding: 10px;
      background-color: #fff;
      margin-bottom: 10px;
      p {
        font-size: 14px;
        margin-bottom: 5px;
        text-align: left;
        img {
          width: 12px;
          vertical-align: middle;
          transform: rotate(180deg);
        }
        img.show-type-all {
          transform: rotate(0deg);
        }
      }
      ul.show-type-all {
        height: auto;
      }
      .f-item-list {
        overflow: hidden;
        display: flex;
        flex-wrap: wrap;
        height: 80px;
        li {
          width: 33.3%;
          span {
            display: block;
            height: 30px;
            background-color: #eee;
            padding-top: 5px;
            padding-left: 3px;
            padding-right: 3px;
            margin-right: 8px;
            margin-top: 10px;
            font-size: 13px;
            border-radius: 6px;
            overflow: hidden;
            white-space: nowrap;
            text-overflow: ellipsis;
            border: 1px solid transparent;
          }
          span.prop-select {
            background-color: #fff;
            color: red;
            border: 1px solid red;
          }
        }
      }
    }
  }
  .right-edit {
    position: absolute;
    bottom: 0;
    left: 0;
    width: 100%;
    button {
      float: left;
      height: 40px;
      width: 50%;
      border: 0 none;
    }
  }
}
.cover {
  background-color: rgba(0, 0, 0, 0.356);
  position: fixed;
  width: 100%;
  height: 100%;
  top: 0;
  left: 0;
  z-index: 6;
}
</style>
