<template>
  <div class="hpCanvas">
    <!-- <transition name="hpmove" appear>
    <h1 class="hptitle">酒 店 管 理 系 统</h1>
    </transition> -->
      <!--返回按钮-->
    
    <transition name="hpmove" appear>
            <div style = "font-family:PingFang SC;
                font-size:20px;
                color:white;
                left: 4%;
                position: absolute;">
                <h1>个 人 中 心</h1>
              </div>
    </transition>

    <transition name="hpmove" appear>
                <div style = "position: absolute;
                left: 87%;
                top:3%">
                <el-avatar icon="el-icon-user-solid" size = "large"></el-avatar>
                </div>
    </transition>

    <transition name="hpmove" appear>
                <div style = "position: absolute;
                left: 90%;
                top:3%">
                <el-button type="primary" icon="el-icon-back" @click="BackPage()">返回</el-button>
                </div>
            <!-- <el-col :span="10"><div class="grid-content bg-purple"></div></el-col> -->
    </transition>
    <el-divider>个人信息</el-divider>
    <div class="intro">
        <el-card class="box-card">
        <div slot="header" class="clearfix">
            <span>个人资料</span>
            <el-button style="float: right; padding: 3px 0" type="text" icon="el-icon-right" @click="JumpToHealthReport()" >前往健康上报</el-button>
        </div>
        <div v-for="info in Info" :key="info" class="text item">
            {{info}}
        </div>
        </el-card>
    </div>
      <div class = "SecondDivider">
        <el-divider>修改密码</el-divider>
      </div>
    <div class="ChangePassword">
      <el-form :model="ruleForm" status-icon :rules="rules" ref="ruleForm" label-width="100px" class="demo-ruleForm">
        <el-form-item label="旧密码" prop="OldPass">
          <el-input type="Password" v-model="ruleForm.OldPass" autocomplete="off"></el-input>
        </el-form-item>
        <el-form-item label="新密码" prop="NewPass">
          <el-input type="Password" v-model="ruleForm.NewPass" autocomplete="off"></el-input>
        </el-form-item>
        <el-form-item label="确认密码" prop="CheckPass">
          <el-input type="Password" v-model="ruleForm.CheckPass" autocomplete="off"></el-input>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="submitReset()">提交</el-button>
          <!-- <el-button @click="resetForm('ruleForm')">重置</el-button> -->
        </el-form-item>
      </el-form>
    </div>

  </div>


    

</template>

<script>
import {PostResetPassword} from '@/api/UserInfo'
import {GetStoreInfo,GetStoreToken} from '@/store/storeInfo'

export default {
    name:'PersonalCenter',
    data(){
      var checkOldPass = (rule, value, callback) => {
        if (!value) {
          return callback(new Error('旧密码不能为空'));
        }
          else {
              callback();
          }
      };
      var validatePass = (rule, value, callback) => {
        if (value === '') {
          callback(new Error('请输入密码'));
        } else {
          if (this.ruleForm.CheckPass !== '') {
            this.$refs.ruleForm.validateField('CheckPass');
          }
          callback();
        }
      };
      var validatePass2 = (rule, value, callback) => {
        if (value === '') {
          callback(new Error('请再次输入密码'));
        } else if (value !== this.ruleForm.NewPass) {
          callback(new Error('两次输入密码不一致!'));
        } else {
          callback();
        }
      };
      return{
        ruleForm: {
          OldPass: '',
          NewPass: '',
          CheckPass: '',
        },
        rules: {
          OldPass:[
            { validator: checkOldPass, trigger: 'blur' }
          ],
          NewPass: [
            { validator: validatePass, trigger: 'blur' }
          ],
          CheckPass: [
            { validator: validatePass2, trigger: 'blur' }
          ],
        },
        Info: {
            ID: "员工ID : " + GetStoreInfo().ID.toString(),
            Name: "姓名 : " + GetStoreInfo().Name.toString(),
            Department: "部门 : " + GetStoreInfo().Department.toString(),
            Gender: "性别 : " + GetStoreInfo().Gender.toString(),
            Age: "年龄 : " + GetStoreInfo().Age.toString(),
            Salary: "薪资(月) : " + GetStoreInfo().Salary.toString(),
            PhoneNum: "联系方式(手机) : " + GetStoreInfo().PhoneNum.toString(),
        }
      }
      
    },
    methods: {
      submitReset() {
        if(this.ruleForm.OldPass == '' || this.ruleForm.CheckPass == '' || this.ruleForm.NewPass == '')
          {this.$alert('请填写所有必填项', {
          confirmButtonText: '确定',
          });
          return;
          }
        
        this.$confirm('是否要提交修改', '提示', {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning'
        }).then(() => {
          let data = {
            tokenValue:GetStoreToken(),
            oldPassword:this.ruleForm.OldPass,
            newPassword:this.ruleForm.NewPass,
          };
          //发送维修信息
          PostResetPassword(data).then(response=>{
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
        })
        }).catch(() => {
          this.$message({
            type: 'info',
            message: '已取消修改'
          });          
        });

      },
      JumpToHealthReport(){
            this.$router.push({path:'/HealthReport'})
        },
      BackPage(){
        this.$router.go(-1);
      }
    }
}
</script>

<!-- <style>

</style> -->
<style scoped>
  .hpCanvas{
    background:url("../assets/img/pc-bg.png");
    width:100%;			
    height:100%;			
    position:fixed;
    background-size:100% 100%;
    /* display: flex; */
    justify-content: center;
    align-items: stretch;
  }
  .hpmove-enter-active{
    animation: hpmove 0.5s ease-in-out;
  }

  @keyframes hpmove{
    from{
      transform:translateY(-500%);
    }
    to{
      transform:translateY(0%);
    }
  }
  .SecondDivider{
    top: 70%;
    position:relative;
  }
  .intro{
    top:20%;
    left: 15%;
    position:absolute;
  }
  .text {
    font-size: 14px;
  }

  .item {
    margin-bottom: 18px;
  }

  .clearfix:before,
  .clearfix:after {
    display: table;
    content: "";
  }
  .clearfix:after {
    clear: both
  }

  .box-card {
    width: 1400px;
    height: 550px;
  }
  .ChangePassword{
    top: 77.5%;
    left: 40%;
    position: absolute;
  }
  .el-row {
    top: 2%;
    margin-bottom: 20px;

  }
  .el-divider{
    top: 8%;
    background-color: #b6d7fb;
    height: 5px;
  }
  .el-col {
    border-radius: 10px;
  }
  .bg-purple-dark {
    background: #99a9bf;
  }
  .bg-purple {
    background: #d3dce6;
  }
  .bg-purple-light {
    background: #e5e9f2;
  }
  .grid-content {
    border-radius: 4px;
    min-height: 36px;
  }
  .row-bg {
    padding: 10px 0;
    background-color: #f9fafc;
  }
</style>