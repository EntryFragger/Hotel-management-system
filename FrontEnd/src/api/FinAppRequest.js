import request from '@/utils/request'

export function GetFinAppAll(data) {
    return request({
        url: '/FinancialStatement/GetAllFinancialStatement',
        method: 'get',
        params: data
    })
}

export function GetFinApp(data) {
    return request({
        url: '/FinancialStatement/GetFinancialStatement',
        method: 'get',
        params: data
    })
}

export function PostFinApp(data) {
    return request({
        url: '/FinancialStatement/ApprovalFinancialStatement',
        method: 'post',
        params: data
    })
}

export function PostNewFinApp(data) {
    return request({
        url: '/FinancialStatement/SubmitFinancialStatement',
        method: 'post',
        params: data
    })
}
