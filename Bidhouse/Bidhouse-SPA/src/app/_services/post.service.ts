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

  getPosts(category:any,searchInput:string,skipCount:any,takeCount:any){
    if(searchInput == "" && category != ""){
      return this.http.get(this.baseUrl+"getPosts",{
        params:{
          category: category,
          skipCount:skipCount,
          takeCount:takeCount
        }
      });
    }
    else if (category == "" && searchInput != "") {
      
      return this.http.get(this.baseUrl+"getPosts",{
        params:{
          searchInput: searchInput,
          skipCount:skipCount,
          takeCount:takeCount
        }
      });
    }

    else{
      return this.http.get(this.baseUrl+"getPosts",{
        params:{
          category:category,
          searchInput:searchInput,
          skipCount:skipCount,
          takeCount:takeCount
        }
      });
    }
  }

  createPost(input:CreatePostInputModel){
   return this.http.post(this.baseUrl,{
     name:input.Name,
     description:input.Description,
     category:parseInt(input.Category),
     location:input.Location,
     time:input.Time,
     price:input.Price
   });
  }

  deletePost(id:string){
    return this.http.delete(this.baseUrl,{
      params: {
        id:id
      }
    });
  }
  }
