import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {  map } from 'rxjs/operators'; 
import {  JwtHelperService } from '@auth0/angular-jwt'
import { ChangePasswordInputModel } from 'src/viewModels/ChangePasswordInputModel';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
baseUrl = "https://localhost:5001/api/auth/";
jwtHelper = new JwtHelperService
public normalizedToken :any;

constructor(private http:HttpClient) { }

login(model:any){
  return this.http.post(this.baseUrl+"login",model)
  .pipe(
    map((response:any)=>{
      const user = response;
      if(user){
        localStorage.setItem('token',user.token);
        this.normalizedToken = this.jwtHelper.decodeToken(user.token);
      }
    })
  )
}

loggedIn(){
  const token = localStorage.getItem('token');
  return !this.jwtHelper.isTokenExpired(token);
}

logout(){
  localStorage.removeItem('token');
}


register(model:any){
  return this.http.post(this.baseUrl+'register',model);
}

changePassword(id:string,input:ChangePasswordInputModel){
  return this.http.put(this.baseUrl+id,input);
}

isAdmin(){
  let result = this.normalizedToken.role == "Admin" ?true:false;
  return result;
}

}
