import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CreatePostInputModel } from 'src/viewModels/CreatePostInputModel';

@Injectable({
  providedIn: 'root'
})
export class PostService {
  baseUrl = "https://localhost:5001/api/Posts/";
  
  constructor(private http:HttpClient) { }
  
  getPost(id:string){
    return this.http.get(this.baseUrl,{
      params:{
        id:id
      }
    })
  }

  getPostsById(id:string){
    return this.http.get(this.baseUrl+"getPostsById",{
      params:{
        id : id
      }
    });
  }

  getPosts(){
    return this.http.get(this.baseUrl+"getPosts");
  }

  createPost(input:CreatePostInputModel){
   return this.http.post(this.baseUrl,input);
  }

  deletePost(id:string){
    return this.http.delete(this.baseUrl,{
      params: {
        id:id
      }
    });
  }
  }
