import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ReportService {
  baseUrl = "https://localhost:5001/api/Reports/";
constructor(private http:HttpClient) { }

reportPost(postId:string,description:string,postType:string){
return this.http.post(this.baseUrl,{
  postId:postId,
  description:description,
  reportType: parseInt(postType)
});
}

getReports(){
  return this.http.get(this.baseUrl+"getReports");
}

getReport(id:string){
  return this.http.get(this.baseUrl+"getReport",{
    params:{
      id:id
    }
  });
}

deleteReport(id:string){
  return this.http.delete(this.baseUrl,{
    params:{
      id:id
    }
  });
}
}
