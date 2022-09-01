<template>
<!--财务报单审批显示-->
    <transition name="emtablemove" appear>
    <div class="emtable">
    <h2 style="color:white;">维护记录</h2>
        <el-table
        :data="tableData"
        height="230"
        border
        style="width: 100%">
        <el-table-column
            align="center"
            prop="mDate"
            label="日期">
        </el-table-column>
        <el-table-column
            align="center"
            prop="itemId"
            label="设备编号">
        </el-table-column>
        <el-table-column
            align="center"
            prop="itemName"
            label="设备名称">
        </el-table-column>
        <el-table-column
            align="center"
            prop="employeeId"
            label="维护人员ID">
        </el-table-column>
        </el-table>
    </div>
    </transition>
</template>

<script>
import {GetMaintenance} from '@/api/EqMainRequest'
import {GetStoreToken} from '@/store/storeInfo'
  export default {
     data() {
      const tableData = [];
      return {
        tableData
      }
    },
    mounted(){
        this.GetMaintenanceDetail();
    },
    methods: {
      GetMaintenanceDetail(){
        let data = {
            tokenValue: GetStoreToken()
        }

        GetMaintenance(data).then(response=>{
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
    }
  }
</script>

<style scoped>
.emtable{
    width: 90%;
    position:absolute;
    top: 10%;    
}

.emtablemove-enter-active{
    animation: emtablemove 1s ease-in-out;
}

@keyframes emtablemove{
    from{
        transform:translateY(200%);
    }
    to{
        transform:translateY(0%);
    }
}
</style>