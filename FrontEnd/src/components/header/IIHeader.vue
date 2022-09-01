<template>
  <div>
    <el-page-header content="收支信息查询" @back = "BackPage()"></el-page-header>
  </div>
</template>

<script>  
  export default {
      methods: {
        BackPage(){
          this.$router.go(-1);
        }
      }
  }
  </script>
