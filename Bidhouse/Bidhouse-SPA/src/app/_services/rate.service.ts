import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class RateService {
  baseUrl = "https://localhost:5001/api/Reviews/";

constructor(private http:HttpClient) { }

reviewUser(id,description,rating){

  return this.http.post(this.baseUrl,{
  ReviewedUserId : id,
  Rating : rating,
  Description : description
});


}

}
