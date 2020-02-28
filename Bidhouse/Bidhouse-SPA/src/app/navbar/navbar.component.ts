import { Component, OnInit } from '@angular/core';
import { LoginInputModel } from 'src/viewModels/loginInputModel';

import { FormControl, FormGroup,ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
 
  model:any = {};
  currentUsername:any;
  loginInputModel :LoginInputModel;

  constructor(private authService:AuthService,private alertify:AlertifyService,private router:Router) { }

  ngOnInit() {
  }

  login(){
    this.authService.login(this.model).subscribe((response)=>{
      this.alertify.success("You have successfully logged in ! ")
      this.router.navigate(['/posts']);
    },error=>{
      this.alertify.error("Cannot find username and password combination")
    });
    
   // this.currentUsername = this.authService.normalizedToken.unique_name
  }

  loggedIn(){
    return this.authService.loggedIn();
  }

  logout(){
    this.authService.logout();
    this.router.navigate(['/home']);
  }
}
