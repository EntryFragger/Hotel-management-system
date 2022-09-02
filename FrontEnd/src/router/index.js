import  VueRouter from 'vue-router';

import MyLogin from '@/views/MyLogin.vue'
import HomePage from '@/views/HomePage.vue'
import PersonalCenter from '@/views/PersonalCenter.vue'
import FinancialReporting from '@/views/FinancialReporting.vue'
import CheckOut from '@/views/CheckOut.vue'
import CustomerInfo from '@/views/CustomerInfo.vue'
import CustomerOrder from '@/views/CustomerOrder.vue'
import EqMaintenance from '@/views/EqMaintenance.vue'
import FinancialApproval from '@/views/FinancialApproval.vue'
import RoomService from '@/views/RoomService.vue'
import BecomeMember from '@/views/BecomeMember.vue'
import ReserveRoom from '@/views/ReserveRoom.vue'
import ReserveRoomDetails from '@/views/ReserveRoomDetails.vue'
import StoreManage from '@/views/StoreManage.vue'
import BuyProduct from '@/views/BuyProduct.vue'
import OrderInf from '@/views/OrderInf.vue'
import IncomeInf from '@/views/IncomeInf.vue'
import CreateRoomService from '@/views/CreateRoomService.vue'
import StaffInfo from '@/views/StaffInfo.vue'
import StaffInfoChange from '@/views/StaffInfoChange.vue'
import StaffInfoNew from '@/views/StaffInfoNew.vue'
import EmployeeManagement from '@/views/EmployeeManagement.vue'
import HealthReport from '@/views/HealthReport.vue'
import GetHealthResult from '@/views/GetHealthResult.vue'

export default new VueRouter({
    routes:[
        {//登录
            name: "MyLogin",
            path:'/',
            component:MyLogin
        },
        {
            name:'HomePage',
            path:'/HomePage',
            component:HomePage
        },
        {
            name:'PersonalCenter',
            path:'/PersonalCenter',
            component:PersonalCenter
        },
        {
            name:'FinancialReporting',
            path:'/FinancialReporting',
            component:FinancialReporting
        },
        {//退房
            name:'CheckOut',
            path:'/CheckOut',
            component:CheckOut
        },
        {//顾客信息
            name:'CustomerInfo',
            path:'/CustomerInfo',
            component:CustomerInfo
        },
        {//顾客订单
            name:'CustomerOrder',
            path:'/CustomerOrder',
            component:CustomerOrder
        },
        {//维护记录
            name:'EqMaintenance',
            path:'/EqMaintenance',
            component:EqMaintenance
        },
        {//财务审批
            name:'FinancialApproval',
            path:'/FinancialApproval',
            component:FinancialApproval
        },
        {//住房服务
            name:'RoomService',
            path:'/RoomService',
            component:RoomService
        },
        {//会员开通
            path: '/BecomeMember',
            name: 'becomeMember',
            component: BecomeMember
        },
        {//订房
            path: '/ReserveRoom',
            name: 'reserveRoom',
            component: ReserveRoom
        },
        {//客房细则及客户信息录入
            path: '/ReserveRoomDetails',
            name: 'reserveRoomDetails',
            component: ReserveRoomDetails
        },
        {//库存管理
            path: '/StoreManage',
            name: 'StoreManage',
            component: StoreManage
        },
        {//采购记录
            path: '/BuyProduct',
            name: 'BuyProduct',
            component: BuyProduct
        },
        {
            name: 'OrderInf',
            path: '/OrderInf',
            component: OrderInf
        },
        {
            name: 'IncomeInf',
            path: '/IncomeInf',
            component: IncomeInf
        },
        {
            name: 'CreateRoomService',
            path: '/CreateRoomService',
            component: CreateRoomService
        },
        {
            name: 'StaffInfo',
            path: '/StaffInfo',
            component: StaffInfo
        },
        {
            name: 'StaffInfoChange',
            path: '/StaffInfoChange',
            component: StaffInfoChange
        },
        {
            name: 'StaffInfoNew',
            path: '/StaffInfoNew',
            component: StaffInfoNew
        },
        {
            name: 'EmployeeManagement',
            path: '/EmployeeManagement',
            component: EmployeeManagement
        },
        {
            name: 'HealthReport',
            path: '/HealthReport',
            component: HealthReport
        },
        {
            name: 'GetHealthResult',
            path: '/GetHealthResult',
            component: GetHealthResult
        }
        
    ]
})