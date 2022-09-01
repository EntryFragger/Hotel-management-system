import request from '@/utils/request'
export function GetPersonInfo(data) {
    return request({
        url: '/Login/Login',
        method: 'get',
        params: data
    })
}