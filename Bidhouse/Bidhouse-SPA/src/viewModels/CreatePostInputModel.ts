import { Injectable } from "@angular/core";


export class CreatePostInputModel {
    
    Name:any;
    Description:any;
    Category:any;
    Location:any;
    Time:any;
    Price:any;

    constructor(name:string,description:string,category:number,location:string,time:any,price:number) {
      this.Name = name,
      this.Description = description,
      this.Category = category;
      this.Location = location,
      this.Time = time,
      this.Price = price;
    }

    
}