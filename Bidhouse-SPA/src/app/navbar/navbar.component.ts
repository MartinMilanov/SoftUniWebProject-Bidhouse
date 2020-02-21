import { Component, OnInit } from '@angular/core';
import { LoginInputModel } from 'src/viewModels/loginInputModel';

import { FormControl, FormGroup,ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
 
 model:any = {};
  
  loginInputModel :LoginInputModel;

  constructor(private authService:AuthService) { }

  ngOnInit() {
  }

  login(){
    this.authService.login(this.model).subscribe((response)=>{
      console.log('Login successfull !')
    },error=>{
      console.log(error)
    });
  }

  loggedIn(){
    return this.authService.loggedIn();
  }

  logout(){
    this.authService.logout();
  }
}
