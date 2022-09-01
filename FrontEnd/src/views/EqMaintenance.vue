<template>
<!--设备维护界面-->
  <div class="embg">
    <transition name="emmove" appear>
    <h1 class="emtitle">设 备 维 护 登 记</h1>
    </transition>
      <!--返回按钮-->
    <transition name="emmove" appear>
    <el-button-group class="emback">
      <el-button type="primary" icon="el-icon-arrow-left" @click="BackPage">返回</el-button>
    </el-button-group>
    </transition>
    
    <EqMainInfo></EqMainInfo>
    <EqMainNew></EqMainNew>

  </div>
</template>

<script>
import EqMainInfo from '@/components/EqMainInfo.vue';
import EqMainNew from '@/components/EqMainNew.vue';
export default {
    name:'EqMaintenance',
    components: { EqMainInfo, EqMainNew},
    methods: {
      BackPage(){
        this.$router.go(-1);
      }
    },
}
</script>

<style scoped>
  .embg{
    background:url("../assets/img/em-bg.jpg");
    width:100%;			
    height:100%;			
    position:fixed;
    background-size:100% 100%;
    display: flex;
    justify-content: center;
    align-items: stretch;
  }
  .emback{
    position:absolute;
    top: 4.7%;
    left: 88.5%;
  }
  .emtitle{
    font-family:"PingFang SC";
    font-size:37px;
    color:white;
    position:absolute;
    top: 0.5%;
    left: 5%;
  }
  .emmove-enter-active{
    animation: emmove 0.5s ease-in-out;
  }

  @keyframes emmove{
    from{
      transform:translateY(-200%);
    }
    to{
      transform:translateY(0%);
    }
  }

</style>