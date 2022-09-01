<template>
<!--登录界面-->
  <div id="login">
  <transition name="titlemove" appear>
  <h1 id="title">酒 店 管 理 系 统 登 录</h1>
  </transition>
  
  <transition name="bodymove" appear>
    <div id="mylogin" align="center">
      <h4 id="logintitle">登 录</h4>
      <el-form
        :model="loginForm"
        :rules="loginRules"
        ref="loginForm"
        label-width="0px"
      >
        <el-form-item label="" prop="account" style="margin-top: 10px">
          <el-row>
            <el-col :span="2">
              <span class="el-icon-s-custom"></span>
            </el-col>
            <el-col :span="22">
              <el-input
                id="inps"
                placeholder="账号"
                v-model="loginForm.account"
              >
              </el-input>
            </el-col>
          </el-row>
        </el-form-item>
        <el-form-item label="" prop="passWord">
          <el-row>
            <el-col :span="2">
              <span class="el-icon-lock"></span>
            </el-col>
            <el-col :span="22">
              <el-input
                id="inps"
                type="password"
                placeholder="密码"
                v-model="loginForm.passWord"
              ></el-input>
            </el-col>
          </el-row>
        </el-form-item>
        <el-form-item style="margin-top: 55px">
          <el-button type="primary" round id="submitBtn" @click="submitForm"
            >登录
          </el-button>
        </el-form-item>
        
      </el-form>
    </div>
    </transition>
  </div>
  
</template>

<script>
import 'animate.css';
import {GetPersonInfo} from '@/api/LoginRequest'
import {StoreStartInfo} from '@/store/storeInfo'

export default {
  name: "MyLogin",
  data: function () {
    return {
      loginForm: {
        account: "",
        passWord: "",
      },
      loginRules: {
        account: [{ required: true, message: "请输入账号", trigger: "blur" }],
        passWord: [{ required: true, message: "请输入密码", trigger: "blur" }],
      },
    };
  },
 
  methods: {
    submitForm() {
      const userAccount = this.loginForm.account;
      const userPassword = this.loginForm.passWord;
      if (!userAccount) {
        return this.$message({
          type: "error",
          message: "账号不能为空！",
        });
      }
      if (!userPassword) {
        return this.$message({
          type: "error",
          message: "密码不能为空！",
        });
      }
      console.log("用户输入的账号为：", userAccount);
      console.log("用户输入的密码为：", userPassword);
      
      let data = {
        ID:parseInt(this.loginForm.account),
        Password:this.loginForm.passWord
      }

      GetPersonInfo(data).then(response=>{
        //获取api中的数据
        console.log(response.value);
        StoreStartInfo(response.value);
        this.$router.push({path:'/HomePage'})

        }).catch((error)=>{
            this.$message({
            message:error.response.data,
            type:'warning'
            });
            console.log('error',error)
            return;
          })
    },
  },
};


</script>

<style scoped>

  #login{
  background:url("../assets/img/ml-bg.jpg");
  width:100%;			
  height:100%;			
  position:fixed;
  background-size:100% 100%;
  display: flex;
  justify-content: center;
  align-items: stretch;
  }

  #title{
    font-family:"PingFang SC";
    font-size:40px;
    color:white;
  }

  #logintitle{
    color:white;
  }
  #mylogin {
    width: 240px;
    height: 280px;
    position: absolute;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    margin: auto;
    padding: 50px 40px 40px 40px;
    box-shadow: -15px 15px 15px rgba(6, 17, 47, 0.7);
    opacity: 1;
    background: linear-gradient(
      230deg,
      rgba(53, 57, 74, 0) 0%,
      rgb(0, 0, 0) 100%
    );
  }
 
  #inps input {
    border: none;
    color: #fff;
    background-color: transparent;
    font-size: 12px;
  }
 
  #submitBtn {
    background-color: transparent;
    color: #39f;
    width: 200px;
  }

  .titlemove-enter-active{
    animation: titlemove 0.5s ease-in-out;
    /*
    animation-name: fadeIn;
    opacity: 1;*/
  }

  .bodymove-enter-active{
     animation: bodymove 1s ease-in-out;
     /*
     animation-name: fadeIn;
     opacity: 1;*/
  }

  @keyframes titlemove{
    from{
      transform:translateY(-20%);
    }
    to{
      transform:translateY(0%);
    }
  }

  @keyframes bodymove{
    from{
      transform:translateY(200%);
    }
    to{
      transform:translateY(0%);
    }
  }

  /*淡入效果不好放弃了
  @keyframes fadeIn {
    0% { opacity: 0 }
    100% { opacity: 1 }
  }*/
</style>