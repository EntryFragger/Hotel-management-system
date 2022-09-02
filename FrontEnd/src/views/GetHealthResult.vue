<template>
    <!--设备维护界面-->
      <div class="ghbg">
        <transition name="ghmove" appear>
        <h1 class="ghtitle">员 工 核 酸 信 息 查 询</h1>
        </transition>
          <!--返回按钮-->
        <transition name="ghmove" appear>
        <el-button-group class="ghback">
          <el-button type="primary" icon="el-icon-arrow-left" @click="BackPage">返回</el-button>
        </el-button-group>
        </transition>
        
        <GetHealthInfo></GetHealthInfo>

      </div>
</template>

<script>
import GetHealthInfo from '@/components/GetHealthInfo.vue';
export default {
    components:{GetHealthInfo},
    name:'StaffManagement',
    methods: {
    BackPage(){
    this.$router.go(-1);
    }
    },
}
</script>

<style scoped>
    .ghbg{
    background:url("../assets/img/ghr-bg.jpg");
    width:100%;			
    height:100%;			
    position:fixed;
    background-size:100% 100%;
    display: flex;
    justify-content: center;
    align-items: stretch;
    }
    .ghback{
    position:absolute;
    top: 4.7%;
    left: 88.5%;
    }
    .ghtitle{
    font-family:"PingFang SC";
    font-size:37px;
    color:white;
    position:absolute;
    top: 0.5%;
    left: 5%;
    }
    .ghmove-enter-active{
    animation: ghmove 0.5s ease-in-out;
    }

    @keyframes ghmove{
    from{
        transform:translateY(-200%);
    }
    to{
        transform:translateY(0%);
    }
    }

</style>