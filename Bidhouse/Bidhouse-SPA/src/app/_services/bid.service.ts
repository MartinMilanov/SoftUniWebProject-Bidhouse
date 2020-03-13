import { Injectable } from '@angular/core';
import { BidInputModel } from 'src/viewModels/BidInputModel';
import { HttpClient } from '@angular/common/http';
import { BidApproveInputModel } from 'src/viewModels/BidApproveInputModel';

@Injectable({
  providedIn: 'root'
})
export class BidService {

  baseUrl = "https://localhost:5001/api/Bids/";

constructor(private http:HttpClient) { }

placeBid(input:BidInputModel){
  return this.http.post(this.baseUrl,input);
}

approveBid(bidId:string,postId:string){
  let queryData = new BidApproveInputModel(bidId,postId);
  
   return this.http.post(this.baseUrl+"approveBid",
     queryData
    
   );
}

getBids(){
  return this.http.get(this.baseUrl);
}

}



