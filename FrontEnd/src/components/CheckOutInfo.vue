<template>
  <transition name="cotablemove" appear>
  <div class="cotable">
  <el-descriptions :column="1" border>
    <template slot="extra">
      <el-button type="primary" size="small" @click="GetRoomInfo">查询</el-button>
      <el-button type="primary" size="small" @click="RoomCheckOut">退房</el-button>
    </template>

    <el-descriptions-item>
      <template slot="label">房间号</template>
      <el-input
      placeholder="请输入房间号"
      v-model="RoomID"
      clearable>
      </el-input>
    </el-descriptions-item>

    <el-descriptions-item>
      <template slot="label">房间类型</template>
      {{item.roomType}}  
    </el-descriptions-item>

    <el-descriptions-item>
      <template slot="label">房间价格</template>
      {{item.roomPrice}}
    </el-descriptions-item>

    <el-descriptions-item>
      <template slot="label">房间状态</template>
      {{item.roomStatus}}
    </el-descriptions-item>
  </el-descriptions>
  </div>
  </transition>
</template>

<script>
import {PostCheckOut, GetRoomDetail} from '@/api/CheckOutRequest'
import {GetStoreToken} from '@/store/storeInfo'
export default {
  data(){
     const item = {
        roomType:'',
        roomStatus:'',
				roomPrice:''
     }
     let RoomID = ''
    return{
        item,
        RoomID
    }
  },
  methods:{
    //获取房屋信息
    GetRoomInfo(){
      let data = {
        tokenValue:GetStoreToken(),
        room_id:this.RoomID
      }
      GetRoomDetail(data).then(response=>{
        //获取api中的数据
        //console.log(response.value);
        this.item = response.value;
        console.log(this.item);

        }).catch((error)=>{
            this.$message({
            message:error,
            type:'warning'
            });
            console.log('error',error)
            return;
          })
    },

    //退房
    RoomCheckOut(){
      //确定退房
      this.$confirm('是否要进行退房', '提示', {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning'
        }).then(() => {
          let param = {
          RoomID:this.RoomID,
          Token:GetStoreToken()
          };
          //发送退房信息
          PostCheckOut(param).then(response=>{
          //获取api中的数据
          console.log(response);

          this.$message({
          type: 'success',
          message: '退房成功'});
          
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

      
    }
}
}
</script>

<style scoped>
.cotable{
    width: 95%;
    position:absolute;
    top: 30%;    
}
.cotablemove-enter-active{
    animation: cotablemove 1.5s ease-in-out;
}

@keyframes cotablemove{
    from{
        transform:translateY(200%);
    }
    to{
        transform:translateY(0%);
    }
}
</style>