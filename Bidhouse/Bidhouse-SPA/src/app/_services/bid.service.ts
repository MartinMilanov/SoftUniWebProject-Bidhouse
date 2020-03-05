import { Injectable } from '@angular/core';
import { BidInputModel } from 'src/viewModels/BidInputModel';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class BidService {

  baseUrl = "https://localhost:5001/api/Bids/";

constructor(private http:HttpClient) { }

placeBid(input:BidInputModel){
  return this.http.post(this.baseUrl,input);
}

}

