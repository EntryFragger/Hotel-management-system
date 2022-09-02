<template>
<div>
    <transition name="ghcardmove" appear>
    <el-card class="ghboxcard">
    <el-form :inline="true">
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
    </transition>

    <transition name="ghtablemove" appear>
    <div class="ghtable">
    <el-table
    :data="tableData"
    height="380"
    border
    style="width: 100%">
    <el-table-column
        prop="employeeID"
        align="center"
        label="员工ID"
        width="242">
    </el-table-column>
    <el-table-column
        prop="name"
        align="center"
        label="员工姓名"
        width="242">
    </el-table-column>
    <el-table-column
        prop="department"
        align="center"
        label="员工部门"
        width="242">
    </el-table-column>
    <el-table-column
        prop="nDate"
        align="center"
        label="检测日期"
        width="242">
    </el-table-column>
    <el-table-column
        prop="result"
        align="center"
        label="核酸结果">
    </el-table-column>
    </el-table>
    </div>
    </transition>

</div>

    

</template>

<script>
import {GetHealthResult} from '@/api/HealthResult'
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

            GetHealthResult(data).then(response=>{
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
        }
        
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
.ghboxcard{
    width: 50%;
    height: 11.5%;
    position:absolute;
    top:20%;
    left:5%;
}


.newinfocard{
    width: 15%;
    height: 3.5%;
    position: absolute;
    top: 20%;
    left: 74%;
    margin: auto;
    padding: 15px 40px 40px 40px;
    box-shadow: -15px 15px 15px rgba(6, 17, 47, 0.7);
    opacity: 1;
    background: rgb(64, 158, 255);
}

.ghtable{
    width: 90%;
    position:absolute;
    top:37%;
    left:5%;
}

.ghcardmove-enter-active{
    animation: ghcardmove 1s ease-in-out;
}

.ghtablemove-enter-active{
    animation: ghtablemove 1.3s ease-in-out;
}

@keyframes ghcardmove{
    from{
        transform:translateY(700%);
    }
    to{
        transform:translateY(0%);
    }
}

@keyframes ghtablemove{
    from{
        transform:translateY(200%);
    }
    to{
        transform:translateY(0%);
    }
}
</style>