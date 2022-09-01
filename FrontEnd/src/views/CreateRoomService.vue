<template>
<div id="login">
  <transition name="titlemove" appear>
  <h1 id="title">客 房 服 务 提 交 系 统</h1>
  </transition>

  <transition name="emnewmove" appear>
  <div class="emnew">
    <el-card class="emboxcard">
    <el-form ref="form" label-width="80px" size="mini">

      <el-form-item label="房间号">
        <el-input v-model="newData.room_id"></el-input>
      </el-form-item>

      <el-form-item label="日期">
        <el-col :span="13">
          <el-date-picker type="date" placeholder="选择日期" v-model="Datetemp" style="width: 100%;" :picker-options="pickerOptions">
          </el-date-picker>
        </el-col>
      </el-form-item>

      <el-form-item label="类型">
        <el-input v-model="newData.type"></el-input>
      </el-form-item>

      <el-form-item label="备注">
        <el-input v-model="newData.remark"></el-input>
      </el-form-item>

      <el-form-item label="金额">
        <el-input v-model="newData.aomunt"></el-input>
      </el-form-item>
      
    <transition name="rsmove" appear>
      <el-button-group class="rssub">
        <el-button type="primary" @click="onSubmit">立即创建</el-button>
        <el-button @click="cleanBox">清空</el-button>
    </el-button-group>
    </transition>
    </el-form>
    </el-card>
  </div>
  </transition>
  <transition name="rsmove" appear>
    <el-button-group class="rsback">
      <el-button type="primary" icon="el-icon-arrow-left" @click="BackPage()">返回</el-button>
    </el-button-group>
  </transition>
</div>
</template>

<script>
import {CreateRoomService} from '@/api/CreateRoomService'
import {GetStoreToken} from '@/store/storeInfo'

export default {
    data() {
        const newData = {
        room_id:'',
        Date:'',
        type:'',
        remark:'',
        aomunt:''        
      };
      let Datetemp = new Date();
      return {
        pickerOptions: {
          disabledDate(time) {
            return time.getTime() > Date.now();
          },
          shortcuts: [{
            text: '今天',
            onClick(picker) {
              picker.$emit('pick', new Date());
            }
          }, {
            text: '昨天',
            onClick(picker) {
              const date = new Date();
              date.setTime(date.getTime() - 3600 * 1000 * 24);
              picker.$emit('pick', date);
            }
          }, {
            text: '一周前',
            onClick(picker) {
              const date = new Date();
              date.setTime(date.getTime() - 3600 * 1000 * 24 * 7);
              picker.$emit('pick', date);
            }
          }]
        },
        newData,
        Datetemp
      }
    },
    methods: {
      onSubmit() {
        this.newData.Date = this.Datetemp.toLocaleDateString();
        if(this.newData.room_id == '' || this.newData.time == ''||this.newData.type=='')
          {this.$alert('请完善服务信息', {
          confirmButtonText: '确定',
          });
          return;
          }
        
        this.$confirm('是否要提交信息', '提示', {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning'
        }).then(() => {
          let data = {
            tokenValue:GetStoreToken(),
            time:this.newData.Date,
            room_id:this.newData.room_id,
            type:this.newData.type,
            remark:this.newData.remark,
            amount:this.newData.aomunt
          };
          //发送维修信息
          CreateRoomService(data).then(response=>{
          //获取api中的数据
          console.log(response);
          if('住房服务创建成功' === response)
          {
            this.$message({
            type: 'success',
            message: '提交住房服务成功'
          });
          }
          else
          {
            this.$message({
            message:'提交住房服务失败',
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
        })//取消退房
        }).catch(() => {
          this.$message({
            type: 'info',
            message: '已取消'
          });          
        });

      },

      // 未完成事项: 修改cleanBox()
      cleanBox(){
        this.$confirm('此操作将清空已填信息, 是否继续?', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        this.newData.Date='';
        this.newData.aomunt='';
        this.newData.remark='';
        this.newData.room_id='';
        this.newData.type='';
        this.$message({
          type: 'success',
          message: '信息已清空'
        });
      }).catch(() => {
        this.$message({
          type: 'info',
          message: '已取消操作'
        });          
      });
      },
      BackPage(){
        this.$router.go(-1);
      }
    }
}
</script>

<style scoped>
.emnew{
  width: 100%;
  top:20%;			
  position:fixed;
  background-size:100% 100%;
  display: flex;
  justify-content: center;
  align-items: stretch;
}

.emboxcard {
  width: 49%;
  height: 250px;
}

.emnewmove-enter-active{
    animation: emnewmove 1.5s ease-in-out;
}

@keyframes emnewmove{
    from{
        transform:translateY(200%);
    }
    to{
        transform:translateY(0%);
    }
}
#login{
  background:url("../assets/img/rs-bg.jpg");
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
    height: 350px;
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
    background-color: white;
    font-size: 12px;
  }
 
  #submitBtn {
    background-color: white;
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
  .rsback{
    position:absolute;
    top: 4.7%;
    left: 88.5%;
  }
  .rssub{
    position:absolute;
    top: 110%;
    left: 45%;
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
@keyframes rsmove{
    from{
      transform:translateY(-200%);
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