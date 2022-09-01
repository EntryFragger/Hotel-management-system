<template>
<!--财务报单审批维护界面-->
  <div class="crbg">
    <transition name="crmove" appear>
    <h1 class="crtitle">顾 客 订 单 查 询</h1>
    </transition>
      <!--返回按钮-->
    <transition name="crmove" appear>
    <el-button-group class="crback">
      <el-button type="primary" icon="el-icon-arrow-left" @click="BackPage">返回</el-button>
    </el-button-group>
    </transition>
    <div class="COI">
      <el-menu
        default-active="/OrderInf"
        class="el-menu-co"
        mode="horizontal"
        :router="true">
        <el-submenu index="order">
          <template slot="title">订单页面</template>
          <el-menu-item index="/OrderInf">全部订单</el-menu-item>
          <el-menu-item index="/CustomerOrder">订单查询</el-menu-item>
        </el-submenu>
        <el-submenu index="rs">
          <template slot="title">下单页面</template>
          <el-menu-item index="/ReserveRoom">客房下单</el-menu-item>
          <el-menu-item index="/CreateRoomService">客房服务下单</el-menu-item>
        </el-submenu>
        <el-menu-item index="/CheckOut">人工退房</el-menu-item>
        <!-- <el-menu-item index="4"><a href="https://www.ele.me" target="_blank">订单管理</a></el-menu-item> -->
      </el-menu>
      <CusOrdInfo></CusOrdInfo>

    </div>

  </div>
</template>

<script>
import CusOrdInfo from '@/components/CusOrdInfo.vue';
export default {
    name:'CustomerOrder',
    components: {  CusOrdInfo },
    methods: {
      BackPage(){
        this.$router.go(-1);
      }
    },
}
</script>

<style scoped>
  .crbg{
    background:url("../assets/img/cr-bg.jpg");
    width:100%;			
    height:100%;			
    position:fixed;
    background-size:100% 100%;
    /* display: flex; */
    justify-content: center;
    align-items: stretch;
  }
  .crback{
    position:absolute;
    top: 4.7%;
    left: 88.5%;
  }
  .crtitle{
    font-family:"PingFang SC";
    font-size:37px;
    color:white;
    position:absolute;
    top: 0.5%;
    left: 5%;
  }
  .crmove-enter-active{
    animation: crmove 0.5s ease-in-out;
  }
  /* .COI{
    position: absolute;
    top: 0%;
    width: 100%;
  } */
  .el-menu-co{
    position: absolute;
    width: 100%;
    top: 10%;
  }

  @keyframes crmove{
    from{
      transform:translateY(-200%);
    }
    to{
      transform:translateY(0%);
    }
  }

</style>