import { Injectable } from "@angular/core";


export class UserUpdateModel {
    
    JobPosition:string;
    City:string;
    Description:string;
    constructor(jobPosition:string,city:string,description:string) {
        this.JobPosition = jobPosition;
        this.Description = description;
        this.City = city;
    }

    
}