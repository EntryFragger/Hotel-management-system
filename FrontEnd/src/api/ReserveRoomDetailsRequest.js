import request from '@/utils/request'

export function PostRoomOrder(param) {
    return request({
        url: '/Customer/CustomerBookRoom',
        method: 'post',
        data: param
    })
}
