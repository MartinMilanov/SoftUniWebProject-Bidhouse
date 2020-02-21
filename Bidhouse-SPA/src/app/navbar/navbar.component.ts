import { Component, OnInit } from '@angular/core';
import { LoginInputModel } from 'src/viewModels/loginInputModel';

import { FormControl, FormGroup,ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
 
 model:any = {};
  
  loginInputModel :LoginInputModel;

  constructor(private authService:AuthService,private alertify:AlertifyService) { }

  ngOnInit() {
  }

  login(){
    this.authService.login(this.model).subscribe((response)=>{
      this.alertify.success("You have successfully logged in ! ")
    },error=>{
      this.alertify.error("Cannot find username and password combination")
    });
  }

  loggedIn(){
    return this.authService.loggedIn();
  }

  logout(){
    this.authService.logout();
  }
}
