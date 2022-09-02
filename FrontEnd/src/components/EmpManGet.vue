<template>
<div>
    <el-card class="eaboxcard">
    <el-form :inline="true"  class="demo-form-inline">
    <el-form-item label="部门">
        <el-select v-model="Department" placeholder="请选择员工部门">
        <el-option
            v-for="item in options"
            :key="item.value"
            :label="item.label"
            :value="item.value">
        </el-option>
        </el-select>
    </el-form-item>
    <el-form-item label="员工ID">
        <el-input v-model="ID" placeholder="请输入内容"></el-input>
    </el-form-item>
    <el-form-item>
        <el-button type="primary" @click="onSubmit">查询</el-button>
    </el-form-item>
    </el-form>
    </el-card>

    <div class="newInfoCard"  @click="newstaffinfo">
        <h1 class="eatitle">新 增 员 工 信 息</h1>
    </div>
    
    <div class="healthinfocard"  @click="jumptohealthinfo">
        <h1 class="eatitle">查 看 核 酸 信 息</h1>
    </div>

    <div class="eatable">
    <el-table
    :data="tableData"
    height="380"
    border
    style="width: 100%">
    <el-table-column
      prop="id"
      align="center"
      label="员工ID"
      width="300">
    </el-table-column>
    <el-table-column
      prop="name"
      align="center"
      label="员工姓名"
      width="300">
    </el-table-column>
    <el-table-column
      prop="department"
      align="center"
      label="员工部门"
      width="300">
    </el-table-column>
    <el-table-column align="center" label="人事操作">
        <template slot-scope="scope">
            <el-button
            size="mini"
            type="success"
            @click="showDetail(scope.row)">查看详情</el-button>
            <el-button
            size="mini"
            type="success"
            @click="changeInfo(scope.row)">修改信息</el-button>
            <el-button
            size="mini"
            type="danger"
            @click="deleteStaff(scope.row)">删除员工</el-button>
        </template>
    </el-table-column>
    </el-table>
    </div>

</div>

  

</template>

<script>
import {GetEmpInfoSimle, DeleteStaff} from '@/api/EmpManRequest'
import {GetStoreToken} from '@/store/storeInfo'
export default {
    methods: {
        onSubmit(){
            let tempDepartment = '';
            console.log(this.Department);
            if(this.Department === '全部')
                tempDepartment = 'ALL';
            else if(this.Department === '财务部')
                tempDepartment = 'Finance';
            else if(this.Department === '管理部')
                tempDepartment = 'Management';
            else if(this.Department === '前台')
                tempDepartment = 'Reception';
            else 
                tempDepartment = 'Logistics';

            let data = {      
                tokenValue:GetStoreToken(),
                department:tempDepartment,
                ID:this.ID
            };

            GetEmpInfoSimle(data).then(response=>{
            //获取api中的数据
            console.log(response.value);
            if(Array.isArray(response.value))
                this.tableData = response.value;
            else
            {
                this.tableData = [];
                this.tableData.push(response.value)
            }
            

            }).catch((error)=>{
                this.$message({
                message:error.response.data,
                type:'warning'
                });
                console.log('error',error)
                return;
            })
        },
        newstaffinfo(){
            this.$router.push({path:'/StaffInfoNew'})
        },
        jumptohealthinfo(){
            this.$router.push({path:'/GetHealthResult'})
        },
        showDetail(row){          
            this.$router.push({
                path:'/StaffInfo',
                name: "StaffInfo",
                params:{
                    ID:row.id
                }
            })
        },
        changeInfo(row){
            this.$router.push({
                path:'/StaffInfoChange',
                name: "StaffInfoChange",
                params:{
                    ID:row.id
                }
            })
        },
        deleteStaff(row){
            
            
        this.$confirm('是否要删除此员工', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
        }).then(() => {
        let data = {
            tokenValue :GetStoreToken(),
            ID :row.id
        }
          //发送信息
        DeleteStaff(data).then(response=>{
        //获取api中的数据
        console.log(response);
        if('删除成功' === response)
        {
        this.$message({
        type: 'success',
        message: '删除成功'
        });
        }
        else
        {
        this.$message({
        message:'删除失败',
        type:'warning'
        });
        }
        }).catch((error)=>{
        this.$message({
        message:error.response.data,
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

        },
    },
    data() {
    let tableData=[];
    return {
        options: [{
            value: '全部',
            label: '全部'
        }, {
            value: '财务部',
            label: '财务部'
        }, {
            value: '管理部',
            label: '管理部'
        }, {
            value: '前台',
            label: '前台'
        }, {
            value: '后勤部',
            label: '后勤部'
        }],
        Department: '全部',
        ID:'NULL',
        tableData
    }
}
}
</script>

<style>
.eaboxcard{
    width: 50%;
    height: 11.5%;
    position:absolute;
    top:20%;
    left:5%;
}


.newInfoCard{
    width: 12%;
    height: 3.5%;
    position: absolute;
    top: 20%;
    left: 75%;
    margin: auto;
    padding: 15px 40px 40px 40px;
    box-shadow: -15px 15px 15px rgba(6, 17, 47, 0.7);
    opacity: 1;
    background: rgb(64, 158, 255);
}
.healthinfocard{
    width: 12%;
    height: 3.5%;
    position: absolute;
    top: 20%;
    left: 57%;
    margin: auto;
    padding: 15px 40px 40px 40px;
    box-shadow: -15px 15px 15px rgba(6, 17, 47, 0.7);
    opacity: 1;
    background: rgb(64, 158, 255);
}

.eatitle{
    font-family:"PingFang SC";
    font-size:30px;
    color:white;
    position:absolute;
    top: 0%;
    left: 10%;
}

.eatable{
    width: 90%;
    position:absolute;
    top:37%;
    left:5%;
}
</style>