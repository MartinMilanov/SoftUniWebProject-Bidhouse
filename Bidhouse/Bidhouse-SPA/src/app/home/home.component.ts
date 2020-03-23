import { Component, OnInit, Output } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  isOnHomePage=true;



  constructor(private authService:AuthService) { }

  ngOnInit() {
  }

  goToRegister(){
    this.isOnHomePage = false;
  }

  declineRegisterHome(homePageMode:boolean){
    this.isOnHomePage = homePageMode;
  }
}
