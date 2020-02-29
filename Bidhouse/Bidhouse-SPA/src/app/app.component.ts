import { Component } from '@angular/core';
import { AuthService } from './_services/auth.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router, NavigationStart, NavigationEnd } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  showSpinner = true;
  title = 'Bidhouse-SPA';
  jwtHelper = new JwtHelperService();
  /**
   *
   */
  constructor(private authService:AuthService,private router:Router){
    this.router.events.subscribe((routerEvent)=>{
      if(routerEvent instanceof NavigationStart){
        this.showSpinner = true;
      }
      if(routerEvent instanceof NavigationEnd){
        this.showSpinner = false;
      }
    })

  }

  ngOnInit(){
    const token = localStorage.getItem('token');
    this.authService.normalizedToken = this.jwtHelper.decodeToken(token);
  }


}
