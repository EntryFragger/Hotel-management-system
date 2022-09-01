<template>
<!--住房服务界面-->
  <div class="rsbg">
    <transition name="rsmove" appear>
    <h1 class="rstitle">住 房 服 务 查 询</h1>
    </transition>
      <!--返回按钮-->
    <transition name="rsmove" appear>
    <el-button-group class="rsback">
      <el-button type="primary" icon="el-icon-arrow-left" @click="BackPage">返回</el-button>
    </el-button-group>
    </transition>
    
    <RoomSerInfo></RoomSerInfo>

  </div>
</template>

<script>
import RoomSerInfo from '@/components/RoomSerInfo.vue';
export default {
    name:'RoomService',
    components: { RoomSerInfo },
    methods: {
      BackPage(){
        this.$router.go(-1);
      }
    },
}
</script>

<style scoped>
  .rsbg{
    background:url("../assets/img/rs-bg.jpg");
    width:100%;			
    height:100%;			
    position:fixed;
    background-size:100% 100%;
    display: flex;
    justify-content: center;
    align-items: stretch;
  }
  .rsback{
    position:absolute;
    top: 4.7%;
    left: 88.5%;
  }
  .rstitle{
    font-family:"PingFang SC";
    font-size:37px;
    color:white;
    position:absolute;
    top: 0.5%;
    left: 5%;
  }
  .rsmove-enter-active{
    animation: rsmove 0.5s ease-in-out;
  }

  @keyframes rsmove{
    from{
      transform:translateY(-200%);
    }
    to{
      transform:translateY(0%);
    }
  }

</style>