<template>

  <el-container class="el-container-config">
  
  
    <el-container>
      <el-header style="height: 160px;text-align:center;background-color: transparent;;">
        <el-row style="height: 50px">
          <el-col>   </el-col>
        </el-row>
      <transition name="rrMove" appear>
        <el-row>
          <el-col :span="22" style="font-size: 50px;text-align:left;line-height:1.5;
          font-family:'Courier New', Courier, monospace;color:white;font-weight:bolder;"> 
            &nbsp;&nbsp;&nbsp;客房下单 
          </el-col>
          <el-col :span="2" style="font-size: 15px"> <el-button @click="GoBackToLastPage" type="primary" icon="el-icon-arrow-left"> 返回 </el-button> </el-col>
        </el-row>
      </transition>
      </el-header>
      
      <el-menu
        default-active="/ReserveRoom"
        class="el-menu-demo"
        mode="horizontal"
        :router="true">
        <el-menu-item index="/OrderInf">订单查询</el-menu-item>
        <el-submenu index="rs">
          <template slot="title">下单页面</template>
          <el-menu-item index="/ReserveRoom">客房下单</el-menu-item>
          <el-menu-item index="/CreateRoomService">客房服务下单</el-menu-item>
        </el-submenu>
        <!-- <el-menu-item index="4"><a href="https://www.ele.me" target="_blank">订单管理</a></el-menu-item> -->
      </el-menu>

      
      <el-main style="text-align: center;height:1200px;font-size: 20px;">
        <el-row>
          <el-col :span="17">
          <transition name="rrTableMove" appear>
            <el-table :data="roomDetailsInfo" border height="700" highlight-current-row
    @current-change="HandleCurrentChange">
              <el-table-column label="客房选择" header-align="center">
                <el-table-column prop="roomID" label="房间号" width="200">
                </el-table-column>
                <el-table-column prop="roomType" label="房间类型" width="400">
                </el-table-column>
                <el-table-column prop="roomStatus" label="房间状态" width="250">
                </el-table-column>
                <el-table-column prop="roomPrice" label="房间价格" width="270">
                </el-table-column>
              </el-table-column>
              
            </el-table>
          </transition>
          </el-col>
        <transition name="rrInfoBlockMove" appear>
          <el-col :push="1" :span="6">
            <!--选择开始日期和结束日期-->
            <DateSelectBlock @GetOrderTime='ModifyTimeInfo'></DateSelectBlock>

            <!--显示订单中房间的信息-->
            <div style="background-color: white;margin-top: 50px;height: 500px;opacity: 0.8;" >
              <span style="font-size: 40px;">客房信息</span>
              <el-row style="height:70px; margin-top: 40px;">
                <el-col :span="10" style="font-weight:bolder;">房间号</el-col>
                <el-col :span="14" style="color:black;">{{chosenRoomInfo.roomID}}</el-col>
              </el-row>
              <el-row style="height:70px;">
                <el-col :span="10" style="font-weight:bolder;">房间类型</el-col>
                <el-col :span="14" style="color:goldenrod;">{{chosenRoomInfo.roomType}}</el-col>
              </el-row>
              <el-row style="height:70px;">
                <el-col :span="10" style="font-weight:bolder;">房间价格</el-col>
                <el-col :span="14" style="color:tomato;">{{chosenRoomInfo.roomPrice}}</el-col>
              </el-row>
              
              <el-row style="height:100px;margin-top: 100px;">
                <el-col :span="12"> <el-button @click="SubmitTheOrder" type="success"> 确认 </el-button> </el-col>
                <el-col :span="10"> <el-button @click="CancelTheSelection" type="danger"> 取消选中 </el-button> </el-col>
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

import DateSelectBlock from '@/components/DateSelectBlock.vue';
import { GetRoomInfo } from '@/api/ReserveRoomRequest';
import {GetStoreToken} from '@/store/storeInfo'


export default {
  name: 'ReserveRoom',
  data() {
    return {
      //订单信息
      roomOrderInfo:{
        startTime: '',
        endTime: '',
        roomOrderID: '',
        voiotations: '',
        orderstatus: ''
      },
      //客房状态信息
      roomDetailsInfo: [],
      //选择的客房的信息
      chosenRoomInfo:{
        roomID: '',
        roomType: '',
        roomStatus: '',
        roomPrice: ''
      },
      //检验订单信息是否完整
      infoComplete: 'no'
    }
    
  },
  components: {
    DateSelectBlock
  },
  created(){
    //获取所有房间的信息
    let data = {
      tokenValue:GetStoreToken(),
      room_type: 'ALL'
    }
    GetRoomInfo(data).then(response=>{
      //获取api中的数据
      console.log(response);
      //如果信息错误
      if('权限不符' === response || '输入房间ID为空' === response || '房间不存在' === response){
        this.$message({
        message:response,
        type:'warning'
        });
        //由于是在页面创建时调用的，所以获取数据失败要返回上一页，一般不会调用到这个语句
        this.$router.go(-1);
        return;
      }
      this.roomDetailsInfo = response.value;

    }).catch((error)=>{
        this.$message({
        message:error.response.data,
        type:'warning'
        });
        console.log('error',error)
        return;
    })
  },
  methods:{
    //在表格中选中客房
    HandleCurrentChange(val){
      this.chosenRoomInfo.roomID = val.roomID;
      this.chosenRoomInfo.roomPrice = val.roomPrice;
      this.chosenRoomInfo.roomStatus = val.roomStatus;
      this.chosenRoomInfo.roomType = val.roomType;
      console.log(val);
      return;
    },
    //检查订单信息完整性
    CheckInfoComplete(){
      if(this.chosenRoomInfo.roomID==''||this.roomOrderInfo.startTime==''||this.roomOrderInfo.endTime==''){
         this.infoComplete = 'no';
       }
       else{
         this.infoComplete = 'yes';
       }
    },
    //确认
    SubmitTheOrder(){
      //先检验订单信息完整性
      this.CheckInfoComplete();
      if(this.infoComplete=='no'){
        this.$message({
            message:'身份信息不完整，请补充信息',
            type:'warning'
        });
        return;
      }
      this.$message({
        message: '客房选择成功',
        type: 'success'
      });
      this.$router.push({
        path: '/ReserveRoomDetails',
        name: 'reserveRoomDetails',
        params:{
          chosenRoomInfo: this.chosenRoomInfo,
          roomOrderInfo: this.roomOrderInfo
        }
      });
      return;
    },
    //取消选中
    CancelTheSelection(){
      this.chosenRoomInfo.roomID = '';
      this.chosenRoomInfo.roomPrice = '';
      this.chosenRoomInfo.roomStatus = '';
      this.chosenRoomInfo.roomType = '';
      return;
    },
    //返回上一页
    GoBackToLastPage(){
      this.$router.go(-1);
      return;
    },
    //将日期选择器的开始日期和结束日期传出来
    ModifyTimeInfo(getData){
      this.roomOrderInfo.startTime = getData.startTime;
      this.roomOrderInfo.endTime = getData.endTime;

    }
  }
}
</script>

<style scoped>

  .el-container-config{
    background: url("../assets/img/ro-bg.jpeg");
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

  .rrMove-enter-active{
    animation: rrMove 0.5s ease-in-out;
  }

  @keyframes rrMove{
    from{
      transform:translateY(-200%);
    }
    to{
      transform:translateY(0%);
    }
  }

  .rrTableMove-enter-active{
    animation: rrTableMove 1s ease-in-out;
  }
  @keyframes rrTableMove{
    from{
      transform:translateX(-200%);
    }
    to{
      transform:translateX(0%);
    }
  }
  
  .rrInfoBlockMove-enter-active{
    animation: rrInfoBlockMove 1s ease-in-out;
  }
  @keyframes rrInfoBlockMove{
    from{
      transform:translateX(300%);
    }
    to{
      transform:translateX(0%);
    }
  }

  
</style>
