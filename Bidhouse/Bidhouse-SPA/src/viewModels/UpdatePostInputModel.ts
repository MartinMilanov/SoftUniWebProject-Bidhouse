import { Injectable } from "@angular/core";

export class UpdatePostInputModel {
    PostId:string;
    Description:string;
    Price:number;
    Location:string;
    Date:Date;

    constructor(postId:string,description:string,price:number,location:string,date:Date) {
        this.Date = date;
        this.Location=location;
        this.Price = price;
        this.PostId = postId;
        this.Description = description;
    }
}
