import request from '@/utils/request'
export function GetOrderInfo(data) {
    return request({
        url: '/Order/Order_GetAll',
        method: 'get',
        params: data
    })
}