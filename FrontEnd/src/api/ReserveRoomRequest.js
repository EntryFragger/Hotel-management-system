import request from '@/utils/request'

export function GetRoomInfo(data) {
    return request({
        url: '/Room/GetRoomInfo_ByType',
        method: 'get',
        params: data
    })
}