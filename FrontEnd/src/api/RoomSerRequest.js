import request from '@/utils/request'

export function GetRoomService(data) {
    return request({
        url: '/RoomService/GetAllRoomService',
        method: 'get',
        params: data
    })
}

export function PostRoomService(param) {
    return request({
        url: '/RoomService/ChangeRoomServiceStatus',
        method: 'post',
        data: param
    })
}