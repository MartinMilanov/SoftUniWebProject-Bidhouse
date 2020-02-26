import { Injectable } from "@angular/core";


export class ChangePasswordInputModel {
    
    ConfirmPassword:string;
    NewPassword:string;
    constructor(confirmPassword:string,newPassword:string) {
        this.ConfirmPassword = confirmPassword;
        this.NewPassword = newPassword;
    }

    
}