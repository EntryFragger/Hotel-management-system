<template>
<!--财务报单审批维护界面-->
  <div class="cobg">
    <transition name="comove" appear>
    <h1 class="cotitle">顾 客 房 间 管 理</h1>
    </transition>
      <!--返回按钮-->
    <transition name="comove" appear>
    <el-button-group class="coback">
      <el-button type="primary" icon="el-icon-arrow-left" @click="BackPage">返回</el-button>
    </el-button-group>
    </transition>
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
        <el-menu-item index="/CheckOut">客房状态管理</el-menu-item>
        <!-- <el-menu-item index="4"><a href="https://www.ele.me" target="_blank">订单管理</a></el-menu-item> -->
      </el-menu>

    
    <CheckOutInfo></CheckOutInfo>

  </div>
</template>

<script>
import CheckOutInfo from '@/components/CheckOutInfo.vue';
export default {
    name:'CheckOut',
    components: { CheckOutInfo },
    methods: {
      BackPage(){
        this.$router.go(-1);
      }
    },
}
</script>

<style scoped>
.cobg{
  background:url("../assets/img/co-bg.jpg");
  width:100%;			
  height:100%;			
  position:fixed;
  background-size:100% 100%;
  display: flex;
  justify-content: center;
  align-items: stretch;
}
.coback{
  position:absolute;
  top: 4.7%;
  left: 88.5%;
}
.cotitle{
  font-family:"PingFang SC";
  font-size:37px;
  color:white;
  position:absolute;
  top: 0.5%;
  left: 5%;
}
.comove-enter-active{
  animation: comove 0.5s ease-in-out;
}
.el-menu-co{
  position: absolute;
  width: 100%;
  top: 12%;
}

@keyframes comove{
  from{
    transform:translateY(-200%);
  }
  to{
    transform:translateY(0%);
  }
}

</style>