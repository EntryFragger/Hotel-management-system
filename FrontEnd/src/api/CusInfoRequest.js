import request from '@/utils/request'
export function GetCustomerInfo(data) {
    return request({
        url: '/Customer/GetCustomerInformation',
        method: 'get',
        params: data
    })
}