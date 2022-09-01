import request from '@/utils/request'

export function GetEmpInfoSimle(data) {
    return request({
        url: '/EmployeeInfor/GetEmployeeSimpleInfor',
        method: 'get',
        params: data
    })
}

export function DeleteStaff(param) {
    return request({
        url: '/EmployeeInfor/DeleteEmployeeInfor',
        method: 'post',
        data: param
    })
}

export function GetStaffInfo(data) {
    return request({
        url: '/EmployeeInfor/GetEmployeeDetailedInfor',
        method: 'get',
        params: data
    })
}

export function AddStaff(param) {
    return request({
        url: '/EmployeeInfor/AddEmployeeInfor',
        method: 'post',
        data: param
    })
}

export function ChangeStaff(param) {
    return request({
        url: '/EmployeeInfor/ChangeEmployeeInfor',
        method: 'post',
        data: param
    })
}