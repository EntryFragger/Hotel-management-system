<template>
  
  <div class="citable">

  <transition name="cimove" appear>
    <el-button-group class="ciback">
      <el-button type="primary"   @click="GetCusInfo">刷新</el-button>
    </el-button-group>
  </transition>

  <transition name="citablemove" appear>
  <el-descriptions  :column="1" border>

    <el-descriptions-item>
      <template slot="label">姓名</template>
      {{item.name}}
    </el-descriptions-item>

    <el-descriptions-item>
      <template slot="label">性别</template>
      {{item.gender}}
    </el-descriptions-item>

    <el-descriptions-item>
      <template slot="label">员工年龄</template>
      {{item.age}}
    </el-descriptions-item>

    <el-descriptions-item>
      <template slot="label">员工薪水</template>
      {{item.salary}}
    </el-descriptions-item>

    <el-descriptions-item>
      <template slot="label">员工手机号</template>
      {{item.phoneNum}}
    </el-descriptions-item>

    <el-descriptions-item>
      <template slot="label">员工部门</template>
      {{item.department}}
    </el-descriptions-item>
  </el-descriptions>
  </transition>
  </div>
  
</template>

<script>
import {GetStaffInfo} from '@/api/EmpManRequest'
import {GetStoreToken} from '@/store/storeInfo'
export default {
  data(){
    const item = {};
    let employeeID = ''
    return{
        item,
        employeeID
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