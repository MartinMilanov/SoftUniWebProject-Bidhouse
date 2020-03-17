import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  baseUrl = "https://localhost:5001/api/Admin/";
constructor(private http:HttpClient) { }

deleteUser(id:string){
  return this.http.delete(this.baseUrl,{
    params:{
      id:id
    }
  });
}

makeAdmin(id:string){
  return this.http.put(this.baseUrl+id,{});
}
}
