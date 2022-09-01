import request from '../utils/request'
export function GetIncomeInfo(data) {
    return request({
        url: '/Account/GetAllAccount',
        method: 'get',
        params: data
    })
}