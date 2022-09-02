import request from '@/utils/request'

export function GetHealthResult(data) {
    return request({
        url: '/NucleicAcidTesting/GetNucleicAcidInfor',
        method: 'get',
        params: data
    })
}

export function PostHealthResult(param) {
    return request({
        url: '/NucleicAcidTesting/SubmitNucleicAcidInfor',
        method: 'post',
        data: param,
    })
}