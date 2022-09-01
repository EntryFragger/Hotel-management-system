
//录入初始数据
export function StoreStartInfo(data)
{
    localStorage.setItem('Authorization',data.token);
    localStorage.setItem('ID',data.id);
    localStorage.setItem('Name',data.name);
    localStorage.setItem('Gender',data.gender);
    localStorage.setItem('Age',data.age);
    localStorage.setItem('Salary',data.salary);
    localStorage.setItem('PhoneNum',data.phoneNum);
    localStorage.setItem('Department',data.department);
}

//获取Token
export function GetStoreToken()
{
    return localStorage.getItem('Authorization');
}

//获取除Token外的信息
export function GetStoreInfo()
{
    return {
        ID:localStorage.getItem('ID'),
        Name:localStorage.getItem('Name'),
        Gender:localStorage.getItem('Gender'),
        Age:localStorage.getItem('Age'),
        Salary:localStorage.getItem('Salary'),
        PhoneNum:localStorage.getItem('PhoneNum'),
        Department:localStorage.getItem('Department')
    }
}

//修改存储的信息(没有Token)
export function ChangeStoreInfo(data)
{
    localStorage.setItem('ID',data.ID);
    localStorage.setItem('Name',data.Name);
    localStorage.setItem('Gender',data.Gender);
    localStorage.setItem('Age',data.Age);
    localStorage.setItem('Salary',data.Salary);
    localStorage.setItem('PhoneNum',data.PhoneNum);
    localStorage.setItem('Department',data.Department);
}

//退出删除所有信息
export function RemoveAllInfo()
{
    localStorage.clear();
}
