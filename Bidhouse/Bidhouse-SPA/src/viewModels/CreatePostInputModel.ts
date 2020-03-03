import { Injectable } from "@angular/core";


export class CreatePostInputModel {
    
    Name:any;
    Description:any;
    Price:any;

    constructor(name:string,description:string,price:number) {
      this.Name = name,
      this.Description = description,
      this.Price = price;
    }

    
}