import { Injectable } from "@angular/core";


export class LoginInputModel {
    
    Username:string;
    Password:string;
    constructor(username:string,password:string) {
        this.Username = username;
        this.Password = password;
    }

    
}