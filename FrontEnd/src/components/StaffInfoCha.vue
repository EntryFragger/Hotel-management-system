<template>
  <div class="citable">
      <transition name="cimove" appear>
        <el-button-group class="ciback">
          <el-button type="primary"   @click="GetCusInfo">刷新</el-button>
        </el-button-group>
      </transition>
      
      <transition name="citablemove" appear>
      <el-descriptions  :column="1" border>
    
      <template slot="extra">
          <el-button type="primary"  @click="newStaffInfo">修改员工信息</el-button>
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
  </transition>
    </div>


  </template>
  
  <script>
  import {ChangeStaff, GetStaffInfo} from '@/api/EmpManRequest'
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
          item,
          employeeID:''
      }
    },
    mounted(){
        this.employeeID = this.$route.params.ID;
        this.GetCusInfo();
    },
    methods:{
        GetCusInfo(){
        let data = {
            tokenValue:GetStoreToken(),
            ID:parseInt(this.employeeID)
        };
        GetStaffInfo(data).then(response=>{
        //获取api中的数据
        console.log(response);
        this.item = response.value;

        }).catch((error)=>{
            this.$message({
            message:error,
            type:'warning'
            });
            console.log('error',error)
            return;
        })
        },
      newStaffInfo(){
          this.$confirm('是否要修改此员工信息', '提示', {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning'
          }).then(() => {
          let data = {
              tokenValue :GetStoreToken(),         
              ID:this.employeeID,
              name:this.item.name,
              gender:this.item.gender,
              age:this.item.age,
              salary:this.item.salary,
              phoneNum:this.item.phoneNum,
              department:this.item.department         
          }
            //发送信息
        ChangeStaff(data).then(response=>{
        //获取api中的数据
        console.log(response);
        if('修改成功' === response)
        {
        this.$message({
        type: 'success',
        message: '修改成功'
        });
        }
        else
        {
        this.$message({
        message:'修改失败',
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

  .cimove-enter-active{
    animation: cimove 0.5s ease-in-out;
  }
  
  @keyframes cimove{
    from{
      transform:translateY(-200%);
    }
    to{
      transform:translateY(0%);
    }
  }
  
  
  .ciback{
    position:absolute;
    top: -18%;
    left: 94.3%;
  }
  </style>