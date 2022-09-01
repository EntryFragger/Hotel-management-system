import request from '../utils/request'
export function CreateRoomService(data) {
    return request({
        url: '/RoomService/CreateRoomService',
        method: 'post',
        data: data
    })
}