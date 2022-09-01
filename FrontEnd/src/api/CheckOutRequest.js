import request from '@/utils/request'

export function PostCheckOut(param) {
    return request({
        url: '/Order/Room_Checkout',
        method: 'post',
        data: param
    })
}

export function GetRoomDetail(data) {
    return request({
        url: '/Room/Room_GetSpecificRoom',
        method: 'get',
        params: data
    })
}