<template>
  <div class="smbg">
    <transition name="smmove" appear>
      <h1 class="smtitle">库 存 管 理</h1>
    </transition>

    <transition name="smmove" appear>
      <el-button-group class="smback">
        <el-button type="primary" icon="el-icon-arrow-left" @click="BackPage">返回</el-button>
      </el-button-group>
    </transition>
    <transition name="smmove" appear>
      <div style = "position: absolute;
      left: 17%;
      top:3%">
      <el-button type="primary" icon="el-icon-arrow-right" @click="JumpToPage('/EqMaintenance')" >转到设备管理</el-button>
      </div>
    </transition>
    <transition name="smmove" appear>
      <div style = "position: absolute;
      left: 24%;
      top:3%">
      <el-button type="primary" icon="el-icon-arrow-right" @click="JumpToPage('/BuyProduct')" >转到采购界面</el-button>
      </div>
    </transition>

    <transition name="smbodymove1" appear>
    <div class="smtable" >
      <h2 class="smtitle2">库 存 列 表</h2>
      <el-table
          :data="tableData"
          height="250"
          style="width: 80%">
        <el-table-column
            prop="name"
            label="名称">
        </el-table-column>
        <el-table-column
            prop="unit"
            label="规格"
        >
        </el-table-column>
        <el-table-column
            prop="quantity"
            label="数量"
        >
        </el-table-column>
      </el-table>
    </div>
  </transition>

  <transition name="smbodymove2" appear>
    <div class="addition">
      <el-form ref="newData1" :model="newData1" label-width="0px">
      <el-form-item >
      <h2 class="smtitle2">新 增 库 存</h2>
      </el-form-item>
      <el-form-item >
        <el-input v-model="newData1.Name" placeholder="请输入物品名称"></el-input>
      </el-form-item>
      <el-form-item >
        <el-input v-model="newData1.Unit" placeholder="请输入物品规格"></el-input>
      </el-form-item>
      <el-form-item >
        <el-input v-model="newData1.Quantity" placeholder="请输入物品数量"></el-input>
      </el-form-item>
      <el-form-item >
        <el-button  type="primary" @click="onupdate">更新库存</el-button>
      </el-form-item>
      </el-form>
    </div>
  </transition>

  <transition name="smbodymove3" appear>
    <div class="change">
      <el-form ref="newData2" :model="newData2" label-width="0px">
      <el-form-item >
      <h2 class="smtitle2">修 改 库 存</h2>
      </el-form-item>
      <el-form-item >
        <el-input v-model="newData2.Name" placeholder="请输入物品名称"></el-input>
      </el-form-item>
      <el-form-item >
        <el-input v-model="newData2.Unit" placeholder="请输入物品规格"></el-input>
      </el-form-item>
      <el-form-item >
        <el-input v-model="newData2.Quantity" placeholder="请输入物品数量"></el-input>
      </el-form-item>
      <el-form-item >
        <el-button  type="primary" @click="onchange">修改库存</el-button>
      </el-form-item>
      </el-form>
    </div>
  </transition>

  <transition name="smbodymove4" appear>
    <div class="delete">
      <el-form ref="delename" :model="delename" label-width="0px">
      <el-form-item >
      <h2 class="smtitle2">删 除 库 存</h2>
      </el-form-item>
      <el-form-item >
        <el-input v-model="delename.deleteName" placeholder="请输入物品名称"></el-input>
      </el-form-item>
      <el-form-item >
        <el-button  type="primary" @click="ondelete">删除库存</el-button>
      </el-form-item>
      </el-form>
    </div>
  </transition>
  </div>

</template>

<script>
import {GetStockInfo, PostChangeStock, PostDeleteStock, PostUpdateStock} from "@/api/StockRequest";
import {GetStoreToken} from '@/store/storeInfo'
export default {
  name: 'StoreManage',
  props: {
    msg: String
  },
  data() {

    const tableData = [];
    const newData1 = {
      Name: '',
      Unit: '',
      Quantity: '',
    };
    const newData2 = {
      Name: '',
      Unit: '',
      Quantity: '',
    };

    return {
      tableData,
      newData1,
      newData2,
      delename:{
        deleteName:''
      },

    }
  },
  mounted() {
    this.GetStockDetail();
  },
  methods: {
    BackPage(){
        this.$router.go(-1);
      },
      JumpToPage(data){
        this.$router.push({path:data})
      },
    GetStockDetail() {
      let data = {
        tokenValue: GetStoreToken()
      }
      GetStockInfo(data).then(response => {
        //获取api中的数据
        console.log(response.value);
        this.tableData = response.value;

      }).catch((error) => {
        this.$message({
          message: error.response.data,
          type: 'warning'
        });
        console.log('error', error)
        return;
      })
    },

    ondelete() {
      if(this.delename.deleteName == '')
      {this.$alert('请完善删除库存信息', {
        confirmButtonText: '确定',
      });
        return;
      }

      this.$confirm('是否要提交删除库存', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        let data = {
          name:this.delename.deleteName,
          tokenValue:GetStoreToken(),
        };
        PostDeleteStock(data).then(response=>{
          //获取api中的数据
          console.log(response);
          if('删除成功' === response)
          {
            this.$message({
              type: 'success',
              message: '提交删除成功',
            });
            this.GetStockInfo();
          }
          else
          {
            this.$message({
              message:'提交删除失败',
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


    onchange() {
      if(this.newData2.name == '')
      {this.$alert('请完善修改库存信息', {
        confirmButtonText: '确定',
      });
        return;
      }

      this.$confirm('是否要提交修改库存', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        let data = {

          name:this.newData2.Name,
          quantity:this.newData2.Quantity,
          unit:this.newData2.Unit,
          tokenValue:GetStoreToken(),
        };
        PostChangeStock(data).then(response=>{
          //获取api中的数据
          console.log(response);
          if('修改完成' === response)
          {
            this.$message({
              type: 'success',
              message: '提交修改成功',
            });
            this.GetStockInfo();
          }
          else
          {
            this.$message({
              message:'提交修改失败',
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

    onupdate() {
      if(this.newData1.name == '')
      {this.$alert('请完善更新库存信息', {
        confirmButtonText: '确定',
      });
        return;
      }

      this.$confirm('是否要提交更新库存', '提示', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        let data = {

          name:this.newData1.Name,
          quantity:this.newData1.Quantity,
          unit:this.newData1.Unit,
          tokenValue:GetStoreToken(),
        };
        PostUpdateStock(data).then(response=>{
          //获取api中的数据
          console.log(response);
          if('添加完成' === response)
          {
            this.$message({
              type: 'success',
              message: '提交更新成功',
            });
            this.GetStockInfo();
          }
          else
          {
            this.$message({
              message:'提交更新失败',
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

.smbg{
  background:url("../assets/img/sm-bg.jpg");
  width:100%;
  height:100%;
  position:fixed;
  background-size:100% 100%;
  display: flex;
  justify-content: left;
  align-items: stretch;
}
.smtable{
  width: 2000px;
  position: absolute;
  top: 12%;
  left: 4%;
}
.smback{
  position:absolute;
  top: 4.7%;
  left: 88.5%;
}
.addition{
  width: 300px;
  height: 320px;
  position: absolute;
  top: 50%;
  left: 5%;
  margin: auto;
  padding: 15px 40px 40px 40px;
  box-shadow: -15px 15px 15px rgba(6, 17, 47, 0.7);
  opacity: 1;
  background: rgb(14, 107, 195);

}
.change{
  width: 300px;
  height: 320px;
  position: absolute;
  top: 50%;
  left: 36%;
  margin: auto;
  padding: 15px 40px 40px 40px;
  box-shadow: -15px 15px 15px rgba(6, 17, 47, 0.7);
  opacity: 1;
  background: rgb(14, 107, 195);
}
.delete{
  width: 300px;
  height: 320px;
  position: absolute;
  top: 50%;
  left: 67%;
  margin: auto;
  padding: 15px 40px 40px 40px;
  box-shadow: -15px 15px 15px rgba(6, 17, 47, 0.7);
  opacity: 1;
  background: rgb(14, 107, 195);
}


 #retn{
  width:100px;
  height:50px;
  position: absolute;
  top: 0px;
  left:1200px;
  right: 0px;
  bottom: 0px;
}

.smtitle{
  font-family:"PingFang SC";
  font-size:37px;
  color:white;
  position:absolute;
  top: 0.5%;
  left: 5%;
}

.smtitle2{
  font-family:"PingFang SC";
  font-size:25px;
  color:white;
}


.smmove-enter-active{
  animation: smmove 0.5s ease-in-out;
}

@keyframes smmove{
  from{
    transform:translateY(-200%);
  }
  to{
    transform:translateY(0%);
  }
}

.smbodymove1-enter-active{
  animation: smbodymove 0.7s ease-in-out;
}

.smbodymove2-enter-active{
  animation: smbodymove 1s ease-in-out;
}

.smbodymove3-enter-active{
  animation: smbodymove 1.2s ease-in-out;
}

.smbodymove4-enter-active{
  animation: smbodymove 1.4s ease-in-out;
}
@keyframes smbodymove{
  from{
    transform:translateY(200%);
  }
  to{
    transform:translateY(0%);
  }
}
</style>
