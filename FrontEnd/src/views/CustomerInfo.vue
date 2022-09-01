<template>
<!--财务报单审批维护界面-->
  <div class="cibg">
    <transition name="cimove" appear>
    <h1 class="cititle">查 询 顾 客 信 息</h1>
    </transition>
      <!--返回按钮-->
    <transition name="cimove" appear>
    <el-button-group class="ciback">
      <el-button type="primary" icon="el-icon-arrow-left"  @click="BackPage">返回</el-button>
    </el-button-group>
    </transition>
    
    <CusInfoGet></CusInfoGet>

  </div>
</template>

<script>
import CusInfoGet from '@/components/CusInfoGet.vue';
export default {
    name:'CustomerInfo',
    components: { CusInfoGet },
    methods: {
      BackPage(){
        this.$router.go(-1);
      }
    },
}
</script>

<style scoped>
  .cibg{
    background:url("../assets/img/ci-bg.jpg");
    width:100%;			
    height:100%;			
    position:fixed;
    background-size:100% 100%;
    display: flex;
    justify-content: center;
    align-items: stretch;
  }
  .ciback{
    position:absolute;
    top: 4.7%;
    left: 88.5%;
  }
  .cititle{
    font-family:"PingFang SC";
    font-size:37px;
    color:white;
    position:absolute;
    top: 0.5%;
    left: 5%;
  }
  .cimove-enter-active{
    animation: cimove 0.5s ease-in-out;
  }

  @keyframes cimove{
    from{
      transform:translateY(-200%);
    }
    to{
      transform:translateY(0%);
    }
  }

</style>