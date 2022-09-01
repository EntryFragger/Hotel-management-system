<template>
  <el-container class="el-container-config">
  
  
    <el-container>
      <!--标题栏-->
      <el-header style="height: 160px;text-align:center;background-color: transparent;;">
        <el-row style="height: 50px">
          <el-col>   </el-col>
        </el-row>
      <transition name="rrdMove" appear>
        <el-row>
          <el-col :span="22" style="font-size: 50px;text-align:left;line-height:1.5;
          font-family:'Courier New', Courier, monospace;color:white;font-weight:bolder;"> 
            &nbsp;&nbsp;&nbsp;订单确认 
          </el-col>
          <el-col :span="2" style="font-size: 15px"> <el-button @click="GoBackToLastPage" type="primary" icon="el-icon-arrow-left"> 返回 </el-button> </el-col>
        </el-row>
      </transition>
      </el-header>
      
      
      
      <el-main style="text-align: center;height:1200px;font-size: 20px;">
        <el-row>
        <transition name="rrdFrameMove" appear>
          <el-col :push="3" :span="10">
            <el-container style="background-color: white;height: 600px;border-radius: 10px;">
              <!--信息录入区-->
              <el-container style="text-align: center;height: 400px" aria-label="信息录入">
                <div style="margin-top:60px;align-items: center;margin-left: 150px;">
                  <el-form :model="guestInfo" :rules="rules" ref="guestInfo" label-width="100px" class="demo-ruleForm">
                    <el-form-item label="姓名" prop="guestName" style="font-size: 30px;color: black;width: 300px;">
                      <el-input v-model="guestInfo.guestName" style="width:200px"></el-input>
                    </el-form-item>
                    <el-form-item label="身份证号" prop="guestID" style="font-size: 30px;color: black;width: 300px;">
                      <el-input v-model="guestInfo.guestID" style="width:200px"></el-input>
                    </el-form-item>
                    <el-form-item label="性别" prop="gender" style="font-size: 30px;color: black;width: 300px;">
                      <el-input v-model="guestInfo.gender" style="width:200px"></el-input>
                    </el-form-item>
                    <el-form-item label="电话号码" prop="phoneNumber" style="font-size: 30px;color: black;width: 300px;">
                      <el-input v-model="guestInfo.phoneNumber" style="width:200px"></el-input>
                    </el-form-item>
                    <el-form-item label="所属区域" prop="area" style="font-size: 30px;color: black;width: 300px;">
                      <el-input v-model="guestInfo.area" style="width:200px"></el-input>
                    </el-form-item>
                  </el-form>
         
                  <el-form :model="roomOrderInfo" :rules="rules" ref="roomOrderInfo" label-width="100px" class="demo-ruleForm">
                    <el-form-item label="开始时间" prop="startTime" style="font-size: 30px;color: black;width: 300px;">
                      <el-input v-model="roomOrderInfo.startTime" style="width:200px" :disabled="true"></el-input>
                    </el-form-item>
                    <el-form-item label="结束时间" prop="endTime" style="font-size: 30px;color: black;width: 300px;">
                      <el-input v-model="roomOrderInfo.endTime" style="width:200px" :disabled="true"></el-input>
                    </el-form-item>

                    <el-form-item>
                      <el-button type="success" @click="SubmitTheOrder">确认</el-button>
                      <el-button type="danger" @click="ClearTheInfo">清空信息</el-button>
                    </el-form-item>

                  </el-form>

                </div>
              </el-container>
            </el-container>
          </el-col>
        </transition>

        <transition name="rrdInfoBlockMove" appear>
          <el-col :push="5" :span="6">
            <div style="background-color: white;height: 280px;opacity: 0.8;" >
              <span style="font-size: 40px;">客房信息</span>
              <el-row style="height:70px; margin-top: 30px;">
                <el-col :span="10" style="font-weight:bolder;">房间号</el-col>
                <el-col :span="14" style="color:black;">{{this.roomInfo.roomID}}</el-col>
              </el-row>
              <el-row style="height:70px;">
                <el-col :span="10" style="font-weight:bolder;">房间类型</el-col>
                <el-col :span="14" style="color:goldenrod;">{{this.roomInfo.roomType}}</el-col>
              </el-row>
              <el-row style="height:70px;">
                <el-col :span="10" style="font-weight:bolder;">房间价格</el-col>
                <el-col :span="14" style="color:tomato;">{{this.roomInfo.roomPrice}}</el-col>
              </el-row>
            </div>

            <div style="background-color: #fad9c1;height: 270px;opacity: 0.8;margin-top: 50px;" >
              <span style="font-size: 35px;">开通VIP</span>
              <el-row style="height:40px; margin-top: 30px;">
                <span style="font-size: 30px; color:#ff8b94">折扣升级</span>
              </el-row>
              <el-row style="height:40px; margin-top: 30px;">
                <span style="font-size: 30px; color:#ffaaa5">服务升级</span>
              <el-row style="height:40px; margin-top: 25px;">
                  <el-button  type="primary" @click="JumpToMemberPage('/BecomeMember')" > 前往开通 </el-button>
              </el-row>

              </el-row>

            </div>
                
          </el-col>
        </transition>
        </el-row>
        
      </el-main>
    </el-container>
  </el-container>
</template>

<script>

import {PostRoomOrder} from '@/api/ReserveRoomDetailsRequest'
import {GetStoreToken} from '@/store/storeInfo'

export default {
  name: 'ReserveRoomDetails',
  data(){
    return{
      //验证信息是否完整
      infoComplete: 'no',
      //客房信息
      roomInfo:{
        roomID: '',
        roomType: '',
        roomStatus: '',
        roomPrice: ''
      },
      //订单信息
      roomOrderInfo:{
        startTime: '',
        endTime: '',
        days: '',
        roomOrderID: '',
        voiotations: '',
        orderstatus: ''
      },
      //客户信息
      guestInfo:{
        guestID: '',
        guestName: '',
        gender: '',
        phoneNumber: '',
        area: ''
      },
      //填写客户信息时的规则
      rules: {
        guestName: [
          { required: true, message: '请输入姓名', trigger: 'blur' },
        ],
        guestID: [
          { required: true, message: '请输入身份证号', trigger: 'blur' }
        ],
        gender: [
          { required: true, message: '请输入性别', trigger: 'blur' }
        ],
        phoneNumber: [
          { required: true, message: '请输入电话号码', trigger: 'blur' }
        ],
        area: [
          { required: true, message: '请输入所属地区', trigger: 'blur' }
        ],
      },
      //客房基本信息
      roomDescription:[
        '38平方米，2张1.35米双人床，不可加床',
        '48平方米，1张1.8米大床，不可加床',
        '20平方米，1张1米单人床，不可加床'
      ]
    }
  },
  methods:{
    //返回上一页
    GoBackToLastPage(){
      this.$router.go(-1);
      return;
    },
    //清空已填信息
    ClearTheInfo(){
      this.$confirm('此操作将清空已填信息, 是否继续?', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        this.guestInfo.guestID='';
        this.guestInfo.guestName='';
        this.guestInfo.phoneNumber='';
        this.guestInfo.gender='';
        this.guestInfo.area='';
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
    CheckInfoComplete(){
      if(this.guestInfo.guestName==''||this.guestInfo.gender==''
       ||this.guestInfo.phoneNumber==''||this.guestInfo.gender==''||this.guestInfo.area==''){
         this.infoComplete = 'no';
       }
       else{
         this.infoComplete = 'yes';
       }
    },
    JumpToMemberPage(data){
      this.$router.push({
        path: data,
        name: 'becomeMember',
        params:{
          chosenRoomInfo: this.$route.params.chosenRoomInfo,
          roomOrderInfo: this.$route.params.roomOrderInfo
        }
      });
    },
    //提交订单
    SubmitTheOrder(){
      this.$confirm('是否提交订单?', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        //先检查信息完整性
        this.CheckInfoComplete();
        if(this.infoComplete == 'no'){
          this.$message({
            message:'身份信息不完整，请补充信息',
            type:'warning'
          });
          return;
        }
        //信息完整后，再进行信息发送
        let param = {
          tokenValue:GetStoreToken(),
          RoomID: this.roomInfo.roomID,
          starttime: this.roomOrderInfo.startTime,
          endtime: this.roomOrderInfo.endTime,
          Days: this.roomOrderInfo.days,
          CustomerID: this.guestInfo.guestID,
          Name: this.guestInfo.guestName,
          Gender: this.guestInfo.gender,
          PhoneNum: this.guestInfo.phoneNumber,
          Area: this.guestInfo.area
        };
        console.log(param);
       
          PostRoomOrder(param).then(response=>{
          //获取api中的数据
          console.log(response);
          //发送成功
          if('预定成功' === response){
            this.$message({
            type: 'success',
            message: '订单提交成功'
            });
          }
          //发送失败
          else{
            this.$message({
            message:'订单提交失败',
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
      //取消操作
      }).catch(() => {
        this.$message({
          type: 'info',
          message: '已取消提交'
        });          
      });
    }
  },
  created(){
    this.roomInfo.roomID = this.$route.params.chosenRoomInfo.roomID;
    this.roomInfo.roomType = this.$route.params.chosenRoomInfo.roomType;
    this.roomInfo.roomPrice = this.$route.params.chosenRoomInfo.roomPrice;
    this.roomOrderInfo.startTime = this.$route.params.roomOrderInfo.startTime;
    this.roomOrderInfo.endTime = this.$route.params.roomOrderInfo.endTime;

    //把相差的毫秒数转换为天数  
    this.roomOrderInfo.days = (new Date(this.roomOrderInfo.endTime) - 0 - new Date(this.roomOrderInfo.startTime) - 0) / 86400000;
  }
}
</script>

<style scoped>
  .el-container-config{
    background: url("../assets/img/rd-bg.jpeg");
    width:100%;			
    height:100%;			
    position:fixed;
    background-size:100% 100%;
    display: flex;
    justify-content: center;
    align-items: stretch;
  }
  .el-header {
    background-color: #B3C0D1;
    color: #333;
    line-height: 60px;
  }

  .rrdMove-enter-active{
    animation: rrdMove 0.5s ease-in-out;
  }

  @keyframes rrdMove{
    from{
      transform:translateY(-200%);
    }
    to{
      transform:translateY(0%);
    }
  }

  .rrdFrameMove-enter-active{
    animation: rrdFrameMove 1s ease-in-out;
  }
  @keyframes rrdFrameMove{
    from{
      transform:translateY(-300%);
    }
    to{
      transform:translateY(0%);
    }
  }
  
  .rrdInfoBlockMove-enter-active{
    animation: rrdInfoBlockMove 1s ease-in-out;
  }
  @keyframes rrdInfoBlockMove{
    from{
      transform:translateY(-300%);
    }
    to{
      transform:translateY(0%);
    }
  }

</style>