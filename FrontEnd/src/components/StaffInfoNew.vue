<template>
  <transition name="citablemove" appear>
  <div class="citable">
  <el-descriptions  :column="1" border>

    <template slot="extra">
        <el-button type="primary"  @click="newStaffInfo">新增员工信息</el-button>
    </template>

    <el-descriptions-item>
      <template slot="label">姓名</template>
      <el-input placeholder="请输入内容" v-model="item.name" clearable></el-input>
    </el-descriptions-item>

    <el-descriptions-item>
      <template slot="label">性别</template>
      <el-input placeholder="请输入内容" v-model="item.gender" clearable></el-input>
    </el-descriptions-item>

    <el-descriptions-item>
      <template slot="label">员工年龄</template>
      <el-input placeholder="请输入内容" v-model="item.age" clearable></el-input>
    </el-descriptions-item>

    <el-descriptions-item>
      <template slot="label">员工薪水</template>
      <el-input placeholder="请输入内容" v-model="item.salary" clearable></el-input>
    </el-descriptions-item>

    <el-descriptions-item>
      <template slot="label">员工手机号</template>
      <el-input placeholder="请输入内容" v-model="item.phoneNum" clearable></el-input>
    </el-descriptions-item>

    <el-descriptions-item>
      <template slot="label">员工部门</template>
      <el-input placeholder="请输入内容" v-model="item.department" clearable></el-input>
    </el-descriptions-item>
  </el-descriptions>
  </div>
  </transition>
</template>

<script>
import {AddStaff} from '@/api/EmpManRequest'
import {GetStoreToken} from '@/store/storeInfo'
export default {
  data(){
    const item = {
        name:'',
        gender:'',
        age:'',
        salary:'',
        phoneNum:'',
        department:''
    };
    return{
        item
    }
  },
  methods:{
    newStaffInfo(){
        this.$confirm('是否要新增此员工', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
        }).then(() => {
        let data = {
            tokenValue :GetStoreToken(),         
            name:this.item.name,
            gender:this.item.gender,
            age:this.item.age,
            salary:this.item.salary,
            phoneNum:this.item.phoneNum,
            department:this.item.department         
        }
          //发送信息
        AddStaff(data).then(response=>{
        //获取api中的数据
        console.log(response);
        if('增加成功' === response)
        {
        this.$message({
        type: 'success',
        message: '增加成功'
        });
        }
        else
        {
        this.$message({
        message:'增加失败',
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
    }
  }
}
</script>

<style>
.citable{
    width: 90%;
    position:absolute;
    top: 25%;    
}
.citablemove-enter-active{
    animation: citablemove 1.5s ease-in-out;
}

@keyframes citablemove{
    from{
        transform:translateY(200%);
    }
    to{
        transform:translateY(0%);
    }
}
</style>