<template>
  <div id="loginPage">
    <div class="login-pad">
      <h2>
        <img src="/img/icons/Logo2.png" alt />
        经销商平台
      </h2>
      <p>
        <input type="text" placeholder="用户名" v-model="customerName"/>
      </p>
      <p>
        <input type="password" placeholder="密码" v-model="pwd"/>
      </p>
      <button @click="login">→</button>
    </div>
    <div class="login-bottom">&copy;朝夕教育科技有限公司</div>
  </div>
</template>

<script>
document.body.style.backgroundColor = "#f8f8f8";
export default {
    data() {
        return {
            customerName:"",
            pwd:"",
        }
    },
    methods:{
        login:function(){
            if (this.customerName=="" ){
                this.$msg("用户名未填写");
                return;
            }
             if ( this.pwd==""){
                this.$msg("密码未填写");
                return;
            }

            this.$http.post("Login",{customerName:this.customerName,pwd:this.pwd}).then(res=>{
              localStorage["dpToken"] = res.data;
              //$router 页面跳转时使用
              //$route  获取当前路由信息
              this.$router.push("/main");
            });
        }
    }
};
</script>

<style lang="scss" scoped>
#loginPage {
  .login-pad {
    text-align: center;
    width: 60%;
    margin: auto;
    margin-top: 25%;
    h2 {
      font-weight: normal;
      margin-bottom: 30px;
      img {
        width: 36px;
        height: 36px;
        background-color: transparent;
        display: inline-block;
        vertical-align: -10px;
      }
    }
    p {
      width: 100%;
      margin-top: 20px;
      input {
        width: 100%;
        height: 36px;
        border: 0 none;
        background-color: transparent;
        text-align: center;
      }
    }
    button {
      margin-top: 36px;
      width: 60px;
      height: 60px;
      border-radius: 30px;
      border: 0 none;
      color: #fff;
      font-size: 26px;
      font-weight: border;
      outline: none;
      background-color: rgb(55, 121, 243);
    }
  }
  .login-bottom {
    position: absolute;
    bottom: 10px;
    text-align: center;
    width: 100%;
    font-size: 14px;
  }
}
</style>