<template>
<div class="bmbg"> 
    <div>  
    <transition name="bmMove" appear>
      <h1 class="bmtitle">顾 客 开 通 会 员</h1>
    </transition>
    <transition name="bmMove" appear>
      <el-button-group class="bmback">
      <el-button @click="GoBackToReservePage" type="primary" icon="el-icon-arrow-left"> 返回 </el-button> 
      </el-button-group>     
    </transition>
    </div> 
      
      
      
        <div class="bmad">
        <transition name="bmAdMove" appear>
          <el-col :push="2" :span="12" style="background-color:antiquewhite;text-align: center;height: 300px;border-style: groove;border-width: 5px;border-color:aqua;">
            <el-row>
              <span style="font-size: 30px;color: gold;">会员尊享</span>
            </el-row>
            
            <el-row style="margin-top: 30px;height:20px">   
              <span style="font-size: 30px;font-family: 'Courier New', Courier, monospace;color: plum;">服务升级</span>
              <span style="font-size: 30px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
              <span style="font-size: 20px;">会员专属前台，提供精致服务</span>
            </el-row>
            
            <el-row style="margin-top: 30px;height:20px">
              <span style="font-size: 30px;font-family: 'Courier New', Courier, monospace;color: plum;">折扣升级</span>
              <span style="font-size: 30px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
              <span style="font-size: 20px;">会员专属折扣，提供优惠价格</span>
            </el-row>
            
            <el-row style="margin-top: 30px;height:20px">
              <span style="font-size: 30px;font-family: 'Courier New', Courier, monospace;color: plum;">体验升级</span>
              <span style="font-size: 30px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
              <span style="font-size: 20px;">会员专属餐饮，体验更多风味</span>
            </el-row>
            
            <el-row style="margin-top: 30px;height:20px">
              <span style="font-size: 30px;font-family: 'Courier New', Courier, monospace;color: plum;">保障升级</span>
              <span style="font-size: 30px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
              <span style="font-size: 20px;">室内监控排查，享受更多保障</span>
            </el-row>

          </el-col>
        </transition>
        </div>
        
        <div class="bmchoice">
          <transition name="bmSelectBlockMove" appear>         
            <!--选择会员等级-->     
              <el-table :data="privilege" border highlight-current-ro @current-change="HandleCurrentChange">
                <el-table-column label="选择会员等级" header-align="center">
                  <el-table-column prop="memberLevel" label="会员等级" width="80">
                  </el-table-column>
                  <el-table-column prop="discount" label="会员折扣" width="80">
                  </el-table-column>
                  <el-table-column prop="gift" label="会员服务" width="320">
                  </el-table-column>                 
                </el-table-column>              
              </el-table>
        </transition>
        </div>

        <div class="bmother">       
            <!--客户信息-->
          <transition name="bmInfoBlockMove" appear>
            <div>
              <!--输入客户身份证号码-->
              <el-form :model="inputData" :rules="rules" ref="inputData" label-width="130px" class="demo-ruleForm">              
                    <el-form-item label="身份证号" prop="inputID" style="font-size: 30px;color: black;">
                      <el-input v-model="inputData.inputID" style="width:250px"></el-input>                     
                    </el-form-item>
                    <el-form-item>
                      <el-button type="primary" @click="SubmitTheID">确认</el-button>
                    </el-form-item>                                     
              </el-form>

              <el-descriptions class="margin-top" :column="1" border style="width:400px;margin-left: 90px;height: 260px;opacity: 0.8;">
                
                <el-descriptions-item :labelStyle='descriptionLabelStyle' :contentStyle='descriptionContentStyle'>
                  <template slot="label" >
                    <i class="el-icon-user"></i>
                    用户名
                  </template>
                  {{guestInfo.guestName}}
                </el-descriptions-item>
                <el-descriptions-item :labelStyle='descriptionLabelStyle' :contentStyle='descriptionContentStyle'>
                  <template slot="label">
                    <i class="el-icon-mobile-phone"></i>
                    手机号
                  </template>
                  {{guestInfo.phoneNumber}}
                </el-descriptions-item>
                <el-descriptions-item :labelStyle='descriptionLabelStyle' :contentStyle='descriptionContentStyle'>
                  <template slot="label">
                    <i class="el-icon-location-outline"></i>
                    所属区域
                  </template>
                  {{guestInfo.area}}
                </el-descriptions-item>
                <el-descriptions-item :labelStyle='descriptionLabelStyle' :contentStyle='descriptionContentStyle'>
                  <template slot="label">
                    <i class="el-icon-medal"></i>
                    会员等级
                  </template>
                  {{memberInfo.memberLevel}}
                </el-descriptions-item>
                <el-descriptions-item :labelStyle='descriptionLabelStyle' :contentStyle='descriptionContentStyle'>
                  <template slot="label">
                    <i class="el-icon-present"></i>
                    优惠券
                  </template>
                  {{memberInfo.coupon}}
                </el-descriptions-item>
              </el-descriptions>
            </div>
          </transition>
        </div>         
      
</div>
</template>

<script>
import { GetGuestInfo } from '@/api/BecomeMemberRequest';
import { PostMemberOrder } from '@/api/BecomeMemberRequest';
import {GetStoreToken} from '@/store/storeInfo'

export default {
  //开通会员
  name: 'BecomeMember',
  data(){
    return{
      memberInfo:{
        memberID: '',
        memberLevel: '',
        //到期时间
        expirationTime: '',
        //优惠券
        coupon: ''
      },
      //为了防止输入错误的信息将已有数据覆盖，先缓存数据
      inputData:{
        inputID: ''
      },
      //客户信息
      guestInfo:{
        guestID: '',
        guestName: '',
        gender: '',
        phoneNumber: '',
        area: ''
      },
      descriptionLabelStyle:{'width': '100px'},
      descriptionContentStyle:{'width': '300px', 'text-align': 'center'},
      //会员特权
      privilege:[
        {
          memberLevel: '1',
          discount: '0.9',
          gift: '开放会员专属前台，提供会员专属餐饮'
        },
        {
          memberLevel: '2',
          discount: '0.7',
          gift: '享受室内偷拍设施专查服务'
        },
        {
          memberLevel: '3',
          discount: '0.5',
          gift: '提升会员专属餐饮品质'
        },
      ],
      //填写客户信息时的规则
      rules: {
        inputID: [
          { required: true, message: '请输入身份证号', trigger: 'blur' }
        ]
      },

    }
  },
  methods:{
    GoBackToReservePage(){
      this.$router.replace({
        path: '/ReserveRoomDetails',
        name: 'reserveRoomDetails',
        params:{
          chosenRoomInfo: this.$route.params.chosenRoomInfo,
          roomOrderInfo: this.$route.params.roomOrderInfo
        }
      });
    },

    //输入身份证号码之后点击“确认”键
    SubmitTheID(){
      this.$confirm('是否查询该客户信息?', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        //获取顾客信息
        let data = {
          tokenValue:GetStoreToken(),
          CustomerID: this.inputData.inputID
        }
        GetGuestInfo(data).then(response=>{
          console.log(response.value);
          //如果信息错误
          if('权限不符' === response || '输入信息有误' === response || '该顾客不存在' === response){
            this.$message({
            message:response,
            type:'warning'
            });
            return;
          }
          //如果信息正确
          this.guestInfo.guestID = this.inputData.inputID;

          this.guestInfo.guestName = response.value.name;
          this.guestInfo.phoneNumber = response.value.phoneNum;
          this.guestInfo.area = response.value.area;
          this.memberInfo.memberLevel = response.value.vipLv;
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
          message: '已取消操作'
        });          
      });
    },
    
    //选中会员等级
    HandleCurrentChange(val){
      this.$confirm('是否开通该等级会员?', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        console.log(val);
        let param = {
          tokenValue:GetStoreToken(),
          CustomerID: this.guestInfo.guestID,
          VipLv: val.memberLevel
        }
        //发送会员申请信息
        PostMemberOrder(param).then(response=>{
          //获取api中的数据
          console.log(response);
          //发送成功
          if('会员开通成功' === response){
            this.$message({
              type: 'success',
              message: '已成功开通该等级会员'
            });
          }
          //发送失败
          else{
            this.$message({
              message:'开通该等级会员失败',
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
          message: '已取消操作'
        });          
      });
    },
  }
}
</script>

<style  scoped>
  .bmbg{
    background: url("../assets/img/bm-bg.jpg");
    width:100%;			
    height:100%;			
    position:fixed;
    background-size:100% 100%;
    display: flex;
    justify-content: center;
    align-items: stretch;
  }
  .bmback{
    position:absolute;
    top: 4.7%;
    left: 88.5%;
  }
  .bmtitle{
    font-family:"PingFang SC";
    font-size:37px;
    color:white;
    position:absolute;
    top: 0.5%;
    left: 5%;
  }
  .bmother{
    width: 481px;
    position:absolute;
    left: 55%;    
    top: 15%;
  }
  
  .bmad{
    width: 100%;
    position:absolute;
    left: -5%;
    top:15%;    
  }
  .bmchoice{
    width: 481px;
    position:absolute;
    left: 3.4%;    
    top: 62%;
  }
  

  .bmMove-enter-active{
    animation: bmMove 0.5s ease-in-out;
  }

  @keyframes bmMove{
    from{
      transform:translateY(-200%);
    }
    to{
      transform:translateY(0%);
    }
  }

  .bmAdMove-enter-active{
    animation: bmAdMove 1s ease-in-out;
  }
  @keyframes bmAdMove{
    from{
      transform:translateX(-300%);
    }
    to{
      transform:translateX(0%);
    }
  }
  
  .bmInfoBlockMove-enter-active{
    animation: bmInfoBlockMove 1s ease-in-out;
  }
  @keyframes bmInfoBlockMove{
    from{
      transform:translateX(200%);
    }
    to{
      transform:translateY(0%);
    }
  }

  .bmSelectBlockMove-enter-active{
    animation: bmSelectBlockMove 1s ease-in-out;
  }
  @keyframes bmSelectBlockMove{
    from{
      transform:translateY(300%);
    }
    to{
      transform:translateY(0%);
    }
  }
</style>