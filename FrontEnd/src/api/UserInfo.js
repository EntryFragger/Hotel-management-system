import request from '@/utils/request'

export function PostResetPassword(param) {

    return request({
        url: '/PersonalInfo/ResetPassword',
        method: 'post',
        data: param,
    })
}

export function GetTaskList(data) {
    return request({
        url: '/RoomService/GetUndoneJob',
        method: 'get',
        params: data
    })
}
