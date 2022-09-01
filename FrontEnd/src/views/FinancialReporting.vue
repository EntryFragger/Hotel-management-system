<template>
  <div class="hpCanvas">
    
    <transition name="hpmove" appear>
            <div style = "font-family:PingFang SC;
                font-size:20px;
                color:white;
                left: 4%;
                position: absolute;">
                <h1>财 务 上 报</h1>
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

    <el-divider>财务报单</el-divider>
    <div class="Financial">
        <el-row>
          <el-table
            :data="FinanData"
            height="500"
            style="width: 100%">
            <el-table-column
                prop="statementID"
                label="财务编号"
                width="550">
            </el-table-column>
            <el-table-column
                prop="statementContent"
                label="财务内容"
                width="550">
            </el-table-column>
            <el-table-column
                prop="amount"
                label="申请数额"
                width="550">
            </el-table-column>
          </el-table>
        </el-row>
    </div>
      <div class = "SecondDivider">
        <el-divider>新建报单</el-divider>
      </div>
    <div class="NewFinan">
        <el-card class="box-card">
        <div slot="header" class="clearfix">
            <span>新建表单</span>
            <el-button style="float: right; padding: 3px 0" type="text" icon="el-icon-plus" @click="SubmitFinan()">提交报单</el-button>
        </div>
        <div >
            <el-input v-model="inputAmount" placeholder="请输入申请数额"></el-input>
        </div>
        <div style="margin-top: 40px;">
            <el-input v-model="inputContent" 
            type="textarea"
            :rows="3" 
            placeholder="请输入申请原因"></el-input>
        </div>

        </el-card>
    </div>

  </div>


    

</template>

<script>
import {GetFinApp,PostNewFinApp} from '@/api/FinAppRequest'
import {GetStoreToken} from '@/store/storeInfo'

export default {
    name:'FinancialReporting',
    data(){
      return{
          FinanData: [],
          inputContent:'',
          inputAmount:''
      }
    },
    mounted(){
        this.GetFinAppDetail()
    },
    methods: {
      GetFinAppDetail(){
        let data = {
            tokenValue:GetStoreToken()
        }

        GetFinApp(data).then(response=>{
        //获取api中的数据
        console.log(response.value);
        this.FinanData = response.value;

        }).catch((error)=>{
            this.$message({
            message:error,
            type:'warning'
            });
            console.log('error',error)
            return;
        })
      },
      open() {
          this.$message({
          message: '提交成功, 请等待审核! ',
          type: 'success'
          });    
      },
      BackPage(){
        this.$router.go(-1);
      },
      SubmitFinan() {
        if(this.inputContent == '' || this.inputAmount == '')
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
            statementContent:this.inputContent,
            amount:this.inputAmount,
            state:'未通过'
          };
          //发送维修信息
          PostNewFinApp(data).then(response=>{
          //获取api中的数据
          console.log(response);
          if('提交财务报单成功' === response)
          {
            this.$message({
            type: 'success',
            message: '提交财务报单成功'
          });
          }
          else
          {
            this.$message({
            message:'提交财务报单失败',
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

        // this.$refs[formName].validate((valid) => {
        //   if (valid) {
        //     alert('submit!');
        //   } else {
        //     console.log('error submit!!');
        //     return false;
        //   }
        // });

      },

    },

}
</script>

<!-- <style>

</style> -->
<style scoped>
  .hpCanvas{
    background:url("../assets/img/fr-bg.png");
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
    top: 62%;
    position:relative;
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
  .Financial{
    top: 15%;
    left: 5%;
    position: absolute;

  }
  .NewFinan{
    top: 70%;
    left: 12%;
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
  .grid-content {
    border-radius: 4px;
    min-height: 36px;
  }
  .row-bg {
    padding: 10px 0;
    background-color: #f9fafc;
  }
</style>