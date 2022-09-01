<template>
    <!--设备维护界面-->
      <div class="eabg">
        <transition name="eamove" appear>
        <h1 class="eatitle">人 事 管 理</h1>
        </transition>
          <!--返回按钮-->
        <transition name="eamove" appear>
        <el-button-group class="eaback">
          <el-button type="primary" icon="el-icon-arrow-left" @click="BackPage">返回</el-button>
        </el-button-group>
        </transition>
        
        <EmpManGet></EmpManGet>

      </div>
</template>

<script>
import EmpManGet from '@/components/EmpManGet.vue';
export default {
    components:{EmpManGet},
    name:'StaffManagement',
    methods: {
    BackPage(){
    this.$router.go(-1);
    }
    },
}
</script>

<style scoped>
    .eabg{
    background:url("../assets/img/si-bg.jpg");
    width:100%;			
    height:100%;			
    position:fixed;
    background-size:100% 100%;
    display: flex;
    justify-content: center;
    align-items: stretch;
    }
    .eaback{
    position:absolute;
    top: 4.7%;
    left: 88.5%;
    }
    .eatitle{
    font-family:"PingFang SC";
    font-size:37px;
    color:white;
    position:absolute;
    top: 0.5%;
    left: 5%;
    }
    .eamove-enter-active{
    animation: eamove 0.5s ease-in-out;
    }

    @keyframes eamove{
    from{
        transform:translateY(-200%);
    }
    to{
        transform:translateY(0%);
    }
    }

</style>