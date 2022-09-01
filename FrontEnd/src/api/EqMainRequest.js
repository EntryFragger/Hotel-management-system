import request from '@/utils/request'

export function GetMaintenance(data) {
    return request({
        url: '/Maintenance/GetMaintenanceInfo',
        method: 'get',
        params: data
    })
}

export function PostMaintence(param) {
    return request({
        url: '/Maintenance/SubmitMaintenanceInfo',
        method: 'post',
        data: param,
    })
}