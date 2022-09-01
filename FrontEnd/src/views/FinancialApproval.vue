<template>
<!--财务报单审批维护界面-->
  <div class="fabg">
    <transition name="famove" appear>
    <h1 class="fatitle">财 务 报 单 审 批</h1>
    </transition>
      <!--返回按钮-->
    <transition name="famove" appear>
    <el-button-group class="faback">
      <el-button type="primary" icon="el-icon-arrow-left" @click="BackPage">返回</el-button>
    </el-button-group>
    </transition>
    
    <FinAppInfo></FinAppInfo>

  </div>
</template>

<script>
import FinAppInfo from '../components/FinAppInfo.vue'
export default {
    name:'FinancialApproval',
    components: { FinAppInfo },
    methods: {
      BackPage(){
        this.$router.go(-1);
      }
    },
}
</script>

<style scoped>
  .fabg{
    background:url("../assets/img/fa-bg.jpg");
    width:100%;			
    height:100%;			
    position:fixed;
    background-size:100% 100%;
    display: flex;
    justify-content: center;
    align-items: stretch;
  }
  .faback{
    position:absolute;
    top: 4.7%;
    left: 88.5%;
  }
  .fatitle{
    font-family:"PingFang SC";
    font-size:37px;
    color:white;
    position:absolute;
    top: 0.5%;
    left: 5%;
  }
  .famove-enter-active{
    animation: famove 0.5s ease-in-out;
  }

  @keyframes famove{
    from{
      transform:translateY(-200%);
    }
    to{
      transform:translateY(0%);
    }
  }

</style>