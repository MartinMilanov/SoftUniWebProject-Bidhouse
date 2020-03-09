import { Injectable } from "@angular/core";


export class BidApproveInputModel {
    BidId:string;
    PostId:string;
    /**
     *
     */
    constructor(bidId:string,postId:string) {
        this.BidId = bidId;
        this.PostId = postId;
        
    }
    
}