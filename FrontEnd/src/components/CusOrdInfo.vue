<template>
<transition name="crtablemove" appear>
  <div class="crtable">
    <el-card class="crboxcard">

    <el-form :inline="true">
        <el-form-item label="顾客身份证">
            <el-input  v-model="InCustomerID" placeholder="顾客身份证"></el-input>
        </el-form-item> 
        <el-form-item>
            <el-button type="primary" @click="onSubmit">查询</el-button>
        </el-form-item>
    </el-form>
   

    <el-table
    :data="tableData"
    style="width: 100%"
    height="400">
        <el-table-column type="expand">
            <template slot-scope="props">
                <el-form label-position="left" inline class="cr-table-expand">
                    <el-form-item label="订单编号">
                        <span>{{ props.row.orderID }}</span>
                    </el-form-item>

                    <el-form-item label="房间号">
                        <span>{{ props.row.roomID }}</span>
                    </el-form-item>

                    <el-form-item label="顾客身份证">
                        <span>{{ props.row.customerID }}</span>
                    </el-form-item>

                    <el-form-item label="开始时间">
                        <span>{{ props.row.startTime }}</span>
                    </el-form-item>

                    <el-form-item label="结束时间">
                        <span>{{ props.row.endTime }}</span>
                    </el-form-item>

                    <el-form-item label="订单状态">
                        <span>{{ props.row.orderStatus }}</span>
                    </el-form-item>

                    <el-form-item label="违规情况">
                        <span>{{ props.row.violation }}</span>
                    </el-form-item>

                    <el-form-item label="订单金额">
                        <span>{{ props.row.amount }}</span>
                    </el-form-item>
                </el-form>
            </template>
        </el-table-column>
        <el-table-column
        label="房间号"
        prop="roomID">
        </el-table-column>

        <el-table-column
        label="开始时间"
        prop="startTime">
        </el-table-column>

        <el-table-column
        label="结束时间"
        prop="endTime">
        </el-table-column>
    </el-table>
    </el-card>
  </div>
</transition>
</template>

<script>
import {GetCustomerOrder} from '@/api/CusOrderRequest';
import {GetStoreToken} from '@/store/storeInfo'
export default {
    data() {
        let InCustomerID = '';
        return {
        tableData: [],
        InCustomerID
      }
    },
    methods: {
      onSubmit() {
        let data = {
            customer_id:this.InCustomerID,
            token_value:GetStoreToken()
        }
        GetCustomerOrder(data).then(response=>{
        //获取api中的数据
        console.log(response.value);
        this.tableData = response.value;

        }).catch((error)=>{
            this.$message({
            message:error,
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
.cr-table-expand {
    font-size: 0;
  }
.cr-table-expand label {
    width: 90px;
    color: #99a9bf;
  }
.cr-table-expand .el-form-item {
    margin-right: 0;
    margin-bottom: 0;
    width: 50%;
}
.crtable{
    width: 95%;
    position:absolute;
    top: 20%;    
    left: 2%;
}
.crboxcard {
    width: 100%;
}
.crtablemove-enter-active{
    animation: crtablemove 1.5s ease-in-out;
}

@keyframes crtablemove{
    from{
        transform:translateY(200%);
    }
    to{
        transform:translateY(0%);
    }
}
</style>