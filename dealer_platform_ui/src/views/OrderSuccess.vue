<template>
  <div>
    <h2>订单添加成功</h2>
   <router-link :to="'OrderConfirm?no='+orderNo"></router-link>
  </div>
</template>

<script>
export default {
    data() {
        return {
            orderNo:""
        }
    },
    mounted() {
      this.orderNo =  this.$route.query.no;
    },
};
</script>

<style lang="scss" scoped>
</style>