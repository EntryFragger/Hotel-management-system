<template>
  <transition name="citablemove" appear>
  <div class="citable">
  <el-descriptions  :column="1" border>
    <template slot="extra">
      <el-button type="primary" size="small"  @click="GetCusInfo">查询</el-button>
    </template>

    <el-descriptions-item>
      <template slot="label">顾客身份证号</template>
      <el-input
      placeholder="请输入顾客身份证号"
      v-model="item.CustomerID"
      clearable>
      </el-input>
    </el-descriptions-item>

    <el-descriptions-item>
      <template slot="label">顾客姓名</template>
      {{item.name}}
    </el-descriptions-item>

    <el-descriptions-item>
      <template slot="label">顾客性别</template>
      {{item.gender}}
    </el-descriptions-item>

    <el-descriptions-item>
      <template slot="label">顾客手机号</template>
      {{item.phoneNum}}
    </el-descriptions-item>

    <el-descriptions-item>
      <template slot="label">顾客来源地</template>
      {{item.area}}
    </el-descriptions-item>

    <el-descriptions-item>
      <template slot="label">顾客会员等级</template>
      {{item.vipLv}}
    </el-descriptions-item>
  </el-descriptions>
  </div>
  </transition>
</template>

<script>
import {GetCustomerInfo} from '@/api/CusInfoRequest'
import {GetStoreToken} from '@/store/storeInfo'
export default {
  data(){
    const item = [{
        name:'',
        gender:'',
        phoneNum:'',
        area:'',
        vipLv:'',
        customerID:''
     }]
    return{
        item
    }
  },
  methods:{
    GetCusInfo(){
      let data = {      
        tokenValue:GetStoreToken(),
        CustomerID:this.item.CustomerID
      };
      GetCustomerInfo(data).then(response=>{
        //获取api中的数据
        console.log(response.value);
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

<style scoped>
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
</style>