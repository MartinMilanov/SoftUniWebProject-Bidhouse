import { Injectable } from "@angular/core";


export class CreatePostInputModel {
    
    Name:any;
    Description:any;
    Location:any;
    Time:any;
    Price:any;

    constructor(name:string,description:string,location:string,time:any,price:number) {
      this.Name = name,
      this.Description = description,
      this.Location = location,
      this.Time = time,
      this.Price = price;
    }

    
}