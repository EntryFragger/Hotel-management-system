import request from '@/utils/request'

export function PostMemberOrder(param) {
    return request({
        url: '/Customer/OpenVip',
        method: 'post',
        data: param
    })
}

export function GetGuestInfo(data) {
    return request({
        url: '/Customer/GetCustomerInformation',
        method: 'get',
        params: data
    })
}