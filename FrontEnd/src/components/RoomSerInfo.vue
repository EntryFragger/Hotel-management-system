<template>
<!--住房服务显示-->
    <transition name="rstablemove" appear>
    <div class="rstable">
        <el-table
        :data="tableData"
        height="520"
        border
        style="width: 100%">
        <el-table-column
            align="center"
            prop="roomID"
            label="房间号"
            width="180">
        </el-table-column>

        <el-table-column
            align="center"
            prop="remark"
            label="备注"
            width="300">
        </el-table-column>

        <el-table-column
            align="center"
            prop="time"
            label="时间"
            width="180">
        </el-table-column>

        <el-table-column
            align="center"
            prop="status"
            label="状态"
            width="180">
        </el-table-column>

        <el-table-column
            align="center"
            prop="type"
            label="类型"
            width="180">
        </el-table-column>

        <el-table-column align="center" label="完成服务">
            <template slot-scope="scope">
                <el-button
                size="mini"
                type="success"
                @click="handleDone(scope.$index, scope.row)">完成</el-button>
            </template>
        </el-table-column>
        </el-table>
    </div>
    </transition>
</template>

<script>
import {GetRoomService, PostRoomService} from '@/api/RoomSerRequest'
import {GetStoreToken} from '@/store/storeInfo'
export default {
data() {

    let tableData = [];
    return {
        tableData
    }
},
mounted(){
    let data = {
        tokenValue:GetStoreToken()
    }
    GetRoomService(data).then(response=>{
    //获取api中的数据
    this.tableData = response.value;
    }).catch((error)=>{
        this.$message({
        message:error,
        type:'warning'
        });
        console.log('error',error)
        return;
    })
   
},
methods: {
    handleDone(index, row) {
    if(row.status === 'UnDone')
    {
        this.$confirm('是否确定完成任务', '提示', {
            confirmButtonText: '确定',
            cancelButtonText: '取消',
            type: 'warning'
            }).then(() => {
            let data = {
                tokenValue:GetStoreToken(),
                room_id:row.roomID,
				time:row.time,
				status:'Done',
            };
            console.log(data);
            //发送审批信息
            PostRoomService(data).then(response=>{
            //获取api中的数据
            console.log(response);
            if('房间服务状态修改成功' === response)
            {
                this.$message({
                type: 'success',
                message: '发送完成成功'               
                });
                row.status = '已完成'
            }
            else
            {
                this.$message({
                message:'发送完成失败',
                type:'warning'
                });
            }
            }).catch((error)=>{
                this.$message({
                message:error,
                type:'warning'
                });
                console.log('error',error)
                return;
            })//取消
            }).catch(() => {
            this.$message({
                type: 'info',
                message: '已取消'
            });          
            });
    }
    }
}
}
</script>

<style scoped>
.rstable{
    width: 90%;
    position:absolute;
    top: 18%;    
}

.rstablemove-enter-active{
    animation: rstablemove 1s ease-in-out;
}

@keyframes rstablemove{
    from{
        transform:translateY(150%);
    }
    to{
        transform:translateY(0%);
    }
}
</style>