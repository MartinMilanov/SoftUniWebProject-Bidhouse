import { Component, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  isOnHomePage=true;



  constructor() { }

  ngOnInit() {
  }

  goToRegister(){
    this.isOnHomePage = false;
  }

  declineRegisterHome(homePageMode:boolean){
    this.isOnHomePage = homePageMode;
  }
}
