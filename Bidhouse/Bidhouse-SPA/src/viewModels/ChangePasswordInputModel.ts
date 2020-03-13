import { Injectable } from "@angular/core";


export class ChangePasswordInputModel {
    CurrentPassword:string;
    ConfirmPassword:string;
    NewPassword:string;
    constructor(currentPassword:string,confirmPassword:string,newPassword:string) {
        this.CurrentPassword = currentPassword;
        this.ConfirmPassword = confirmPassword;
        this.NewPassword = newPassword;
    }

    
}