<template>
  <div class="block">
    <el-date-picker
      v-model="value"
      type="daterange"
      range-separator="至"
      start-placeholder="开始日期"
      end-placeholder="结束日期"
      @change="EmitTheData"
      value-format="yyyy-MM-dd"
      :picker-options="pickerOptions">
    </el-date-picker>
  </div>
</template>

<script>
  export default {
    name: 'DateSelectBlock',
    data() {
      return {
        value: '',
        emitData:{
          startTime: '',
          endTime: ''
        },
        pickerOptions: {
          disabledDate(time) {
            return time.getTime() < Date.now() - 3600 * 1000 * 24;
          }
        }
      };
        
    },
    methods:{
      //传值到父组件中
      EmitTheData(){
        this.emitData.startTime = this.value[0];
        this.emitData.endTime = this.value[1];
        
        this.$emit('GetOrderTime', this.emitData);
      }
    }
  };
</script>