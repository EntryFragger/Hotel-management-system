<template>
    <div class="hpCanvas">
      
      <transition name="hpmove" appear>
              <div style = "font-family:PingFang SC;
                  font-size:20px;
                  color:white;
                  left: 4%;
                  position: absolute;">
                  <h1>健 康 上 报</h1>
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
                  <el-button type="primary" icon="el-icon-back" @click="BackPage()" >返回</el-button>
                  </div>
              <!-- <el-col :span="10"><div class="grid-content bg-purple"></div></el-col> -->
      </transition>
  
        <div class = "SecondDivider">
          <el-divider>健 康 上 报</el-divider>
        </div>

        <transition name="hrcardmove" appear>
            <div class="NewReport">
                <el-card class="box-card">
                <div slot="header" class="clearfix">
                    <span>提交结果</span>
                    <el-button style="float: right; padding: 3px 0" type="text" icon="el-icon-plus" @click="SubmitReport()" >提交结果</el-button>
                </div>
                <div >
                </div>
                <div >
                    <el-input class = HealthName
                        placeholder = ""
                        v-model="Name"
                        :disabled="true">
                    </el-input>   
                    <el-input class = HealthID
                        placeholder = ""
                        v-model="ID"
                        :disabled="true">
                    </el-input>          
                    <el-select class="HealthSelect" v-model="HealthResult" placeholder="请选择核酸检测结果">
                        <el-option
                            v-for="item in options"
                            :key="item.value"
                            :label="item.label"
                            :value="item.value">
                        </el-option>
                    </el-select>
                </div>
                </el-card>
            </div>
        </transition>
    </div>
  
  
      
  
  </template>
  
  <script>
  import {PostHealthResult} from '@/api/HealthResult'
  import {GetStoreToken,GetStoreInfo} from '@/store/storeInfo'
  
  export default {
      name:'HealthReport',
      data(){
        return{
            Name:'',
            ID:'',
            NowDate:'',

            options:[
                {
                value: 'positive',
                label: '阳性'
                }, 
                {
                value: 'negative',
                label: '阴性'
                }
            ],
            HealthResult:''
        }
      },
      mounted(){
          this.Name = GetStoreInfo().Name;
          this.ID = GetStoreInfo().ID;
          var today = new Date;
          this.NowDate = today.toLocaleDateString();
      },
      methods: {
        BackPage(){
          this.$router.go(-1);
        },
        SubmitReport() {
          if(this.HealthResult == '')
            {this.$alert('请填写所有必填项', {
            confirmButtonText: '确定',
            });
            return;
            }
          
          this.$confirm('是否要提交表单', '提示', {
            confirmButtonText: '确定',
            cancelButtonText: '取消',
            type: 'warning'
          }).then(() => {
            let data = {
              tokenValue:GetStoreToken(),
              ID:GetStoreInfo().ID,
              department:GetStoreInfo().Department,
              result:this.HealthResult,
              date:this.NowDate
            };
            console.log(data);
            //发送维修信息
            PostHealthResult(data).then(response=>{
            //获取api中的数据
            console.log(response);
            if('提交成功' === response)
            {
              this.$message({
              type: 'success',
              message: '提交健康上报成功'
            });
            }
            else
            {
              this.$message({
              message:'提交健康上报失败',
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
              message: '已取消'
            });          
          });
  
        },
  
      },
  
  }
  </script>
  
  <!-- <style>
  
  </style> -->
  <style scoped>
    .hpCanvas{
      background:url("../assets/img/hr-bg.jpeg");
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
    .HealthName{
        position:absolute;
        top: 30%;
        left: 45%;
        width: 16%;
    }
    .HealthID{
        position:absolute;
        top: 50%;
        left: 45%;
        width: 16%;
    }
    .SecondDivider{
      top: 15%;
      position:relative;
    }
    .HealthSelect{
        position:absolute;
        left: 45%;
        top: 70%;
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
      height: 300px;
    }
    .ChangePassword{
      top: 77.5%;
      left: 40%;
      position: absolute;
    }
    .NewReport{
      top: 30%;
      left: 12%;
      position: absolute;
  
    }
    .el-row {
      top: 2%;
      margin-bottom: 20px;
  
    }
    .el-col {
      border-radius: 10px;
    }
    .grid-content {
      border-radius: 4px;
      min-height: 36px;
    }
    .row-bg {
      padding: 10px 0;
      background-color: #f9fafc;
    }

    .hrcardmove-enter-active{
    animation: hrcardmove 1.25s ease-in-out;
    }

    @keyframes hrcardmove{
        from{
            transform:translateY(250%);
        }
        to{
            transform:translateY(0%);
        }
    }
  </style>