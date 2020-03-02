import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ChangePasswordInputModel } from 'src/viewModels/ChangePasswordInputModel';
import { UserUpdateModel } from 'src/viewModels/UserUpdateModel';
import { GetUserQueryInput } from 'src/viewModels/GetUserQueryInput';

@Injectable({
  providedIn: 'root'
})
export class UserService {

baseUrl = "https://localhost:5001/api/Users/";

constructor(private http:HttpClient) { }

getUser(id:string){
  return this.http.get(this.baseUrl+id);
}

getUsers(input:GetUserQueryInput){
  return this.http.get(this.baseUrl+"getUsers",{
    params: {
      startAt:input.StartAt,
      count:input.Count,
      searchInput: input.SearchInput
    }
  });
}


updateUser(id:string,input:UserUpdateModel,formData:any){
  return this.http.post(this.baseUrl+id,formData);
}
}
