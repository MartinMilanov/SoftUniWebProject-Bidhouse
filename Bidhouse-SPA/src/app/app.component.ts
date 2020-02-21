import { Component } from '@angular/core';
import { AuthService } from './_services/auth.service';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Bidhouse-SPA';
  jwtHelper = new JwtHelperService();
  /**
   *
   */
  constructor(private authService:AuthService ){}

  ngOnInit(){
    const token = localStorage.getItem('token');
    this.authService.normalizedToken = this.jwtHelper.decodeToken(token);
  }
}
