<template>
  <div class="maxBox">
    <!-- 顶部 -->
    <div class="header">
      <headers />
    </div>
    <div class = "Navigation">
      <div class="line"></div>
      <el-menu
        default-active="/OrderInf"
        class="el-menu-demo"
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
        <el-menu-item index="/CustomerInfo">顾客查询</el-menu-item>
        <el-menu-item index="/CheckOut">人工退房</el-menu-item>
        <!-- <el-menu-item index="4"><a href="https://www.ele.me" target="_blank">订单管理</a></el-menu-item> -->
      </el-menu>
    </div>
    <!-- 内容 -->
    <el-card class="box-card">
      <div class="table">
        <el-table :data="tableData" style="width: 100%">
          <el-table-column label="订单号"  prop="orderID" align="center"></el-table-column>
          <el-table-column label="房间号"  prop="roomID" align="center"></el-table-column>
          <el-table-column label="用户ID"  prop="customerID" align="center"></el-table-column>
          <el-table-column label="开始时间"  prop="startTime" align="center"></el-table-column>
          <el-table-column label="结束时间"  prop="endTime" align="center"></el-table-column>
          <el-table-column label="入住天数"  prop="days" align="center"></el-table-column>
          <el-table-column label="订单状态"  prop="orderStatus" align="center"></el-table-column>
          <el-table-column label="违例"  prop="violation" align="center"></el-table-column>
          <el-table-column label="金额"  prop="amount" align="center"></el-table-column>
        </el-table>
      </div>
    </el-card>
    <!-- <div class="dialog">
      <dialogBox @handleClick="handleClick" :dialogVisible="dialogVisible"/>
    </div> -->

    <div>
      <el-pagination
        style="margin-top:20px;float: right;"
        :page-sizes="[20, 50, 100]"
        :page-size="100"
        layout="total, sizes, prev, pager, next, jumper"
        :total="1">
      </el-pagination>
    </div>
  </div>
</template>
<script>
import headers from '../components/header/OIHeader.vue'
import {GetOrderInfo} from '@/api/orderRequest'
import {GetStoreToken} from '@/store/storeInfo'
export default {
  components:{
    headers,
  },
   data() {
    // const tableData = [];
      return {
        tableData: []
      }
},
  created() {}, // 生命周期-创建完成
  mounted() {
    this.GetOrderDetail();
  }, // 生命周期-创建之前
  methods: {
    GetOrderDetail(){
        let data = {
            token_value: GetStoreToken()
        }

        GetOrderInfo(data).then(response=>{
        //获取api中的数据
        console.log(response.value);
        this.tableData = response.value;
        }).catch((error)=>{
            this.$message({
            message:error.response.data,
            type:'warning'
            });
            console.log('error',error)
            return;
        })
      }
  },
};
</script>

<style scoped>
  .maxBox {
    background: url('../assets/img/oi-bg.jpg')no-repeat;
    background-size: 100% 100%;
    min-height: 100%;
    height: 100vh;
  }
  .maxBox .header {
    color:white;
    width: 100%;
    height: 50px;
    margin-bottom: 30px;
    display: flex;
    align-items: center;
    padding: 0 20px;
    box-sizing: border-box;
  }
  .maxBox .table {
    color:white;
    padding: 30px 0;
    box-sizing: border-box;
    
  }
</style>