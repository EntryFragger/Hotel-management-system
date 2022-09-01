import request from '@/utils/request'
export function GetCustomerOrder(data) {
    return request({
        url: '/Order/ListOrderByGuest',
        method: 'get',
        params: data
    })
}