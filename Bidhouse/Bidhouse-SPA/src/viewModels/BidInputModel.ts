import { Injectable } from "@angular/core";


export class BidInputModel {
    
    Days:any;
    Price:any;
    Description:any;
    PostId:any;
    ReceiverId:any;
    
    constructor(days:any,price:any,description:any,postId:any,receiverId:any) {
        this.Days = days;
        this.Price = price;
        this.Description = description;
        this.PostId = postId;
        this.ReceiverId = receiverId;
    }

    
}