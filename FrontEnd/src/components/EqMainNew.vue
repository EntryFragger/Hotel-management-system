<template>
  <transition name="emnewmove" appear>
  <div class="emnew">
    <h2 style="color:white;">新增维护信息</h2>
    <el-card class="emboxcard">
    <el-form ref="form" label-width="80px" size="mini">

      <el-form-item label="设备编号">
        <el-input v-model="newData.ItemID"></el-input>
      </el-form-item>

      <el-form-item label="设备名称">
        <el-input v-model="newData.ItemName"></el-input>
      </el-form-item>

      <el-form-item label="维护人ID">
        <el-input v-model="newData.ID"></el-input>
      </el-form-item>

      <el-form-item label="维护日期">
        <el-col :span="13">
          <el-date-picker type="date" placeholder="选择日期" v-model="Datetemp" style="width: 100%;" :picker-options="pickerOptions">
          </el-date-picker>
        </el-col>
      </el-form-item>
      
      <el-form-item size="medium">
        <el-button type="primary" @click="onSubmit">立即创建</el-button>
        <el-button @click="cleanBox">清空</el-button>
      </el-form-item>
    </el-form>
    </el-card>
  </div>
  </transition>
</template>

<script>
import {PostMaintence} from '@/api/EqMainRequest'
import {GetStoreToken} from '@/store/storeInfo'

export default {
    data() {
        const newData = {
        Date:'',
        ItemID:'',
        ID:'',
        ItemName:''        
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
        if(this.newData.ItemID == '' || this.newData.ID == '')
          {this.$alert('请完善维护信息', {
          confirmButtonText: '确定',
          });
          return;
          }
        
        this.$confirm('是否要提交维修', '提示', {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning'
        }).then(() => {
          let data = {
            tokenValue:GetStoreToken(),
            date:this.newData.Date,
            itemID:this.newData.ItemID,
            employeeID:parseInt(this.newData.ID),
            itemName:this.newData.ItemName
          };
          //发送维修信息
          PostMaintence(data).then(response=>{
          //获取api中的数据
          console.log(response);
          if('信息提交成功' === response)
          {
            this.$message({
            type: 'success',
            message: '提交维修成功'
          });
          }
          else
          {
            this.$message({
            message:'提交维修失败',
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

      cleanBox(){
        this.$confirm('是否要提交维修', '提示', {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning'
        }).then(() => {
          this.newData.ID = '';
          this.newData.ItemID = '';
          this.newData.ItemName = '';
          this.$message({
            type: 'success',
            message: '已取消'
          });
        }).catch(() => {
          this.$message({
            type: 'info',
            message: '已取消'
          });          
        });
      }
    }
}
</script>

<style scoped>
.emnew{
  width: 90%;
  position:absolute;
  top: 52%; 
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
</style>