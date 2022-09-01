import request from '@/utils/request'
export function GetStockInfo(data) {
    return request({
        url: '/Stock/Stock_GetAll',
        method: 'get',
        params: data
    })
}

export function PostUpdateStock(param) {

    return request({
        url: '/Stock/UpdateNewStock',
        method: 'post',
        data: param,
    })
}

export function PostDeleteStock(param) {

    return request({
        url: '/Stock/DeleteStock',
        method: 'post',
        data: param,
    })
}

export function PostChangeStock(param) {

    return request({
        url: '/Stock/ChangeStock',
        method: 'post',
        data: param,
    })
}