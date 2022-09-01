<template>

  <div class="bpbg">

      <transition name="bpmove" appear>
        <h1 class="bptitle">库 存 管 理</h1>
      </transition>
  
      <transition name="bpmove" appear>
        <el-button-group class="bpback">
          <el-button type="primary" icon="el-icon-arrow-left" @click="BackPage">返回</el-button>
        </el-button-group>
      </transition>

    <transition name="bpbodymove1" appear>
      <div class="bpnew">
        <el-form ref="form" :model="form" label-width="0px">
        <el-form-item >
        <h2 class="bptitle2">新 增 物 资 采 购</h2>
        </el-form-item>
        <el-form-item >
          <el-input v-model="newData.ItemID" placeholder="请输入商品编码"></el-input>
        </el-form-item>
        <el-form-item >
          <el-input v-model="newData.Goods_name" placeholder="请输入商品名称"></el-input>
        </el-form-item>
        <el-form-item >
          <el-input v-model="newData.Unit" placeholder="请输入商品单位"></el-input>
        </el-form-item>
        <el-form-item >
          <el-input v-model="newData.Price" placeholder="请输入采购价格"></el-input>
        </el-form-item>
        <el-form-item >
          <el-input v-model="newData.Quantity" placeholder="请输入采购数量"></el-input>
        </el-form-item>
        <el-form-item >
          <el-input v-model="newData.Date" placeholder="请输入采购时间"></el-input>
        </el-form-item>
        <el-form-item >
          <el-button  type="primary" icon="el-icon-check" @click="onSubmit">提交采购</el-button>
        </el-form-item>
        </el-form>
      </div>
    </transition>

    <transition name="bpbodymove2" appear>
    <div class="bptable">
      <h2 class="bptitle2">采购清单</h2>
          <el-table
            :data="tableData"
            height="500px"
            style="width: 85%">
          <el-table-column
              prop="purchaseID"
              label="编号">
          </el-table-column>
          <el-table-column
              prop="goodsName"
              label="名称">
          </el-table-column>
            <el-table-column
                prop="unit"
                label="单位"
            >
          </el-table-column>
          <el-table-column
              prop="price"
              label="单价"
              >
          </el-table-column>
          <el-table-column
              prop="quantity"
              label="数量"
              >
          </el-table-column>
          <el-table-column
              prop="pdate"
              label="日期"
          >
          </el-table-column>
        </el-table>
    </div>
  </transition>

  </div>
</template>

<script>

import {GetPurchaseInfo, PostPurchse} from "@/api/PucharseRequest";
import {GetStoreToken} from '@/store/storeInfo';

export default {
  name: 'BuyProduct',
  props: {
    msg: String,
  },
  data() {
    const tableData = [];
    const newData = {
      ItemID: '',
      Goods_name:'',
      Unit:'',
      Quantity:'',
      Price:'',
      Date: '',
    };
    let Datetemp = new Date();
    return {
      tableData,
      newData,
      Datetemp,
      form:{}
    }
  },
  mounted(){
    this.GetPurchaseDetail();
  },
  methods: {
      BackPage(){
        this.$router.go(-1);
      },
    GetPurchaseDetail(){
      let data = {
        token_value:GetStoreToken()
      }
      GetPurchaseInfo(data).then(response=>{
        //获取api中的数据
        console.log(response.value);
        this.tableData = response.value;

      }).catch((error)=>{
        this.$message({
          message:error.response.data,
          type:'warning'
        });
        console.log('error',error)
        return;
      })
    },

    onSubmit() {
      this.newData.Date = this.Datetemp.toLocaleDateString();
      if(this.newData.ItemID == '')
      {this.$alert('请完善采购信息', {
        confirmButtonText: '确定',
      });
        return;
      }

      this.$confirm('是否要提交采购', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        let data = {

          goods_name:this.newData.Goods_name,
          quantity:this.newData.Quantity,
          price:this.newData.Price,
          date:this.newData.Date,
          unit:this.newData.Unit,
          token_value:GetStoreToken()

        };
        //发送维修信息
        PostPurchse(data).then(response=>{
          //获取api中的数据
          console.log(response);
          if('收购信息创建成功' === response)
          {
            this.$message({
              type: 'success',
              message: '提交采购成功',
            });
            this.GetPurchaseDetail();
          }
          else
          {
            this.$message({
              message:'提交采购失败',
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


  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
  <style scoped>

  #submit{
    width:50px;
    height:50px;
    position: absolute;
    top: 250px;
    left: 0px;
    right: 0px;
    bottom: 0px;
  }
  #comp{
    width:100px;
    height:50px;
    position: absolute;
    top: 200px;
    left: 900px;
    right: 0px;
    bottom: 0px;
  }
  #retn1{
    width:100px;
    height:50px;
    position: absolute;
    top: 0px;
    left:1200px;
    right: 0px;
    bottom: 0px;
  }
  #info{
    position: absolute;

  }

.bptable{
  width: 60%;
  position: absolute;
  top: 13%;
  left: 5%;
}
.bpbg{

  background:url("../assets/img/sm-bg.jpg");
  width:100%;
  height:100%;
  position:fixed;
  background-size:100% 100%;
  display: flex;
  justify-content: left;
  align-items: stretch;
}

.bpmove-enter-active{
  animation: bpmove 0.5s ease-in-out;
}

@keyframes bpmove{
  from{
    transform:translateY(-200%);
  }
  to{
    transform:translateY(0%);
  }
}

.bptitle{
  font-family:"PingFang SC";
  font-size:37px;
  color:white;
  position:absolute;
  top: 0.5%;
  left: 5%;
}

.bptitle2{
  font-family:"PingFang SC";
  font-size:25px;
  color:white;
}

.bpback{
  position:absolute;
  top: 4.7%;
  left: 88.5%;
}

.bpnew{
  width: 25%;
  height: 72%;
  position: absolute;
  top: 18%;
  left: 64.1%;
  margin: auto;
  padding: 10px 40px 40px 40px;
  box-shadow: -15px 15px 15px rgba(6, 17, 47, 0.7);
  opacity: 1;
  background: rgb(14, 107, 195);
}

.el-dropdown-link {
  cursor: pointer;
  color: #409EFF;
}
.el-icon-arrow-down {
  font-size: 12px;
}

.bpbodymove1-enter-active{
  animation: bpbodymove 1.3s ease-in-out;
}

.bpbodymove2-enter-active{
  animation: bpbodymove 1s ease-in-out;
}

@keyframes bpbodymove{
  from{
    transform:translateY(200%);
  }
  to{
    transform:translateY(0%);
  }
}
</style>
