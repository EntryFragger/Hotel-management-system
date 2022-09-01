<template>
    <!--财务报单审批显示-->
        <transition name="fatablemove" appear>
        <div class="fatable">
            <el-table
            :data="tableData"
            height="520"
            border
            style="width: 100%">
            <el-table-column
                align="center"
                prop="statementID"
                label="财务报单编号"
                width="180">
            </el-table-column>
            <el-table-column
                align="center"
                prop="statementContent"
                label="报单详情"
                width="450">
            </el-table-column>
            <el-table-column
                align="center"
                prop="amount"
                label="金额(元)"
                width="180">
            </el-table-column>
            <el-table-column
                align="center"
                prop="state"
                label="状态"
                width="180">
            </el-table-column>
            <el-table-column align="center" label="处理方式">
                <template slot-scope="scope">
                    <el-button
                    size="mini"
                    type="success"
                    @click="handlePass(scope.$index, scope.row)">通过</el-button>
                    <el-button
                    size="mini"
                    type="danger"
                    @click="handleRefuse(scope.$index, scope.row)">拒绝</el-button>
                </template>
            </el-table-column>
            </el-table>
        </div>
        </transition>
    </template>
    
    <script>
    import {GetStoreToken} from '@/store/storeInfo'
    import {GetFinAppAll,PostFinApp} from '@/api/FinAppRequest'
    
    export default {
        data() {
            let tableData = []
            return {
            tableData
            }
        },
        mounted(){
            this.GetFinAppAllDetail()
        },
        methods: {
            GetFinAppAllDetail(){
                let data = {
                    token_value:GetStoreToken()
                }
    
                GetFinAppAll(data).then(response=>{
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
            },
    
            handlePass(index, row) {
            console.log(index);
            console.log(row);
            console.log(row.statementID);
            console.log(this.tableData[index]);
            if(row.State != '已通过' && row.State != '已拒绝')
            {
                this.$confirm('是否要通过审批', '提示', {
                confirmButtonText: '确定',
                cancelButtonText: '取消',
                type: 'warning'
                }).then(() => {
                let data = {
                token_value:GetStoreToken(),
                sID:row.statementID};
    
                //发送审批信息
                PostFinApp(data).then(response=>{
                //获取api中的数据
                console.log(response);
                if('审批成功' === response)
                {
                    this.$message({
                    type: 'success',
                    message: '通过审批成功'               
                    });
                    row.State = '已通过'
                }
                else
                {
                    this.$message({
                    message:'通过审批失败',
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
            },
    
            handleRefuse(index, row) {
            if(row.State != '已通过')
                this.$confirm('是否要拒绝审批', '提示', {
                confirmButtonText: '确定',
                cancelButtonText: '取消',
                type: 'warning'
                }).then(() => {
                row.State = '已拒绝';
                this.$message({
                    type: 'success',
                    message: '已拒绝'
                });
                }).catch(() => {
                this.$message({
                    type: 'info',
                    message: '已取消'
                });          
                });
    
            }
        }
    }
    </script>
    
    <style scoped>
    .fatable{
    width: 90%;
    position:absolute;
    top: 18%;    
    }
    
    .fatablemove-enter-active{
    animation: fatablemove 1s ease-in-out;
    }
    
    @keyframes fatablemove{
        from{
            transform:translateY(150%);
        }
        to{
            transform:translateY(0%);
        }
    }
    </style>