import request from '@/utils/request'
export function GetPurchaseInfo(data) {
    return request({
        url: '/Purchase/GetAllPurchase',
        method: 'get',
        params: data
    })
}

export function PostPurchse(param) {

    return request({
        url: '/Purchase/Purchase_Create',
        method: 'post',
        data: param,
    })
}