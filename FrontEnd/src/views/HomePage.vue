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
                <h1>酒 店 管 理 系 统</h1>
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
                <el-button type = "danger" @click="LogOut()">退出登录</el-button>
                </div>
            <!-- <el-col :span="10"><div class="grid-content bg-purple"></div></el-col> -->
    </transition>


    <el-divider>个人简介及任务列表</el-divider>
    <div class="intro">
        <el-row>
          <el-table
            :data="IntroData"
            style="width: 100%">
            <el-table-column
                prop="ID"
                label="员工ID"
                width="550">
            </el-table-column>
            <el-table-column
                prop="name"
                label="姓名"
                width="550">
            </el-table-column>
            <el-table-column
                prop="department"
                label="部门"
                width="550">
            </el-table-column>
          </el-table>
        </el-row>
    </div>

      <div class="Task">
        <el-row>
          <el-table
            :data="TaskData"
            height="500"
            style="width: 100%">
            <el-table-column
                prop="roomID"
                label="房间号"
                width="550">
            </el-table-column>
            <el-table-column
                prop="time"
                label="时间"
                width="550">
            </el-table-column>
            <el-table-column
                prop="type"
                label="任务类型"
                width="550">
            </el-table-column>
          </el-table>
        </el-row>
      </div>
      <div class = "SecondDivider">
        <el-divider>页面导航</el-divider>
      </div>

        <div class="Entrance" >
          <el-row class="Entr" :gutter="400">
            <transition name = "NavigationMove1" appear>
              <el-col :span="6"><div>
              <el-button class="ButtonClass" type="primary" @click="JumpToPage('/PersonalCenter')" >个人中心</el-button>
            </div></el-col>
            </transition>
            <transition name = "NavigationMove2" appear>
              <el-col :span="6"><div>
              <el-button class="ButtonClass" type="success" @click="JumpToPage('/FinancialReporting')">财务上报</el-button>
            </div></el-col>
            </transition>
            <transition name = "NavigationMove3" appear>
              <el-col :span="6"><div>
              <el-button class="ButtonClass" type="warning" v-if="JumpTable[0]" @click="JumpToPage(JumpTable[0].Path)" >{{JumpTable[0].Name}}</el-button>
            </div></el-col>
            </transition>
            <transition name = "NavigationMove4" appear>
              <el-col :span="6"><div>
              <el-button class="ButtonClass" type="danger" v-if="JumpTable[1]" @click="JumpToPage(JumpTable[1].Path)" >{{JumpTable[1].Name}}</el-button>
            </div></el-col>
            </transition>

          </el-row>
        </div>

  </div>


    

</template>

<script>
import {GetTaskList} from '@/api/UserInfo'
import {GetStoreInfo, GetStoreToken, RemoveAllInfo} from '@/store/storeInfo'

export default {
    name:'HomePage',
    data() {
        return {
          IntroData: [{
            // ID: '114514',
            // name: '潘硕',
            // department: '前台'
            ID: GetStoreInfo().ID,
            name: GetStoreInfo().Name,
            department: GetStoreInfo().Department
          }],
          TaskData: [],
          JumpTable: []
        }
      },
    mounted(){
        this.GetTaskDetail();
    },
    methods:{
      GetTaskDetail(){
        let data = {
            tokenValue:GetStoreToken()
        }

        GetTaskList(data).then(response=>{
        //获取api中的数据
        console.log(response.value);
        this.TaskData = response.value;
        }).catch((error)=>{
          //考虑删除
            this.$message({
            message:error,
            type:'warning'
            });
            console.log('error',error)
            return;
        })

        if(GetStoreInfo().Department == 'Finance')
        {
          this.JumpTable = [
            {
              Path: '/FinancialApproval',
              Name: '财务审批'
            },
            {
              Path: '/IncomeInf',
              Name: '收支查询'
            }
          ];
        }
        else if(GetStoreInfo().Department == 'Logistics')
        {
          this.JumpTable = [
            {
              Path: '/RoomService',
              Name: '住房服务'
            },
            {
              Path: '/StoreManage',
              Name: '库存管理'
            }
          ];
        }
        else if(GetStoreInfo().Department == 'Management')
        {
          this.JumpTable = [
            {
              Path: '/EmployeeManagement',
              Name: '人事管理'
            },
            {
              Path: '/StaffInfoNew',
              Name: '信息录入'
            }
          ];
        }
        else if(GetStoreInfo().Department == 'Reception')
        {
          this.JumpTable = [
            {
              Path: '/ReserveRoom',
              Name: '客房下单'
            },
            {
              Path: '/OrderInf',
              Name: '信息查看'
            }
          ];
        }

      },
      LogOut(){
        RemoveAllInfo();
        this.$router.go(-1);
      },
      JumpToPage(data){
        this.$router.push({path:data})
      }
    }
}
</script>

<!-- <style>

</style> -->
<style scoped>
  .hpCanvas{
    background:url("../assets/img/hp-bg.png");
    width:100%;			
    height:100%;			
    position:fixed;
    background-size:100% 100%;
    /* display: flex; */
    justify-content: center;
    align-items: stretch;
  }
  .hpback{
    position:absolute;
    top: 4.7%;
    left: 88.5%;
  }
  .hptitle{
    font-family:"PingFang SC";
    font-size:20px;
    color:white;
    position:absolute;
    top: 0.5%;
    left: 5%;
  }
  .hpmove-enter-active{
    animation: hpmove 0.5s ease-in-out;
  }

  @keyframes hpmove{
    from{
      transform:translateY(-200%);
    }
    to{
      transform:translateY(0%);
    }
  }
  .SecondDivider{
    top: 72.5%;
    position:relative;
  }
  .ButtonClass{
    width: 150px;
    height: 150px;
  }
  .el-divider{
    top: 8%;
    background-color: #b6d7fb;
    height: 5px;
  }

  .intro{
    top:15%;
    left: 5%;
    position:absolute;
  }
  .Task{
    top:30%;
    left: 5%;
    position:absolute;
  }
  .Entrance{
    top:80%;
    left: 2%;
    position:absolute;
  }

.NavigationMove1-enter-active{
  animation: NavigationMove 0.7s ease-in-out;
}

.NavigationMove2-enter-active{
  animation: NavigationMove 1s ease-in-out;
}

.NavigationMove3-enter-active{
  animation: NavigationMove 1.2s ease-in-out;
}

.NavigationMove4-enter-active{
  animation: NavigationMove 1.4s ease-in-out;
}
@keyframes NavigationMove{
  from{
    transform:translateY(200%);
  }
  to{
    transform:translateY(0%);
  }
}


</style>