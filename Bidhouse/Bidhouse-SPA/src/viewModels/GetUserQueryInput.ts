import { Injectable } from "@angular/core";


export class GetUserQueryInput {
    
    StartAt:any;
    Count:any;
    SearchInput:any;

    constructor(startAt:number,count:number,searchInput:any) {
      this.StartAt = startAt,
      this.Count = count,
      this.SearchInput = searchInput;
    }

    
}