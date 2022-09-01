<template>
  <div class="maxBox">
    <!-- 顶部 -->
    <div class="header">
      <headers />
    </div>
    <!-- 内容 -->
    <el-card class="box-card">
      <div class="table">
        <el-table :data="tableData" style="width: 100%">
          <el-table-column label="订单编号"  prop="accountID" align="center"></el-table-column>
          <el-table-column label="日期"  prop="adate" align="center"></el-table-column>
          <el-table-column label="金额"  prop="amount" align="center"></el-table-column>
          <el-table-column label="类型"  prop="type" align="center"></el-table-column>
        </el-table>
      </div>
    </el-card>

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
import headers from '../components/header/IIHeader.vue'
import {GetIncomeInfo} from '@/api/IncomeRequest'
import {GetStoreToken} from '@/store/storeInfo'
export default {
  components:{
    headers,
  },
  data() {
    const tableData = [];
      return {
        tableData
      }
},
  created() {}, // 生命周期-创建完成
  mounted() {
    this.GetIncomeDetail();
  }, // 生命周期-创建之前
  methods: {
    GetIncomeDetail(){
        let data = {
            token_value: GetStoreToken()
        }
        GetIncomeInfo(data).then(response=>{
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
    background: url('../assets/img/co-bg.jpg')no-repeat;
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