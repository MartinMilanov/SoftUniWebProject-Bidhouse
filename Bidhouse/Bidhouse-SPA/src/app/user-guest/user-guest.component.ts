import { Component, OnInit, ViewChild } from '@angular/core';
import { UserService } from '../_services/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';
import { TabsetComponent } from 'ngx-bootstrap';

@Component({
  selector: 'app-user-guest',
  templateUrl: './user-guest.component.html',
  styleUrls: ['./user-guest.component.css']
})
export class UserGuestComponent implements OnInit {
  user:any;
 
  @ViewChild('staticTabs', { static: false }) staticTabs: TabsetComponent;


  constructor(private userService:UserService,private route:ActivatedRoute,private authService:AuthService,private router:Router) { }

  ngOnInit() {
    let id = this.route.snapshot.paramMap.get('id');
    if(id == this.authService.normalizedToken.nameid){
      this.router.routeReuseStrategy.shouldReuseRoute = () => false;
      this.router.onSameUrlNavigation = 'reload';
      this.router.navigate(['/user']);
    }

    else{
      
      this.userService.getUser(id).subscribe(result=>{
        this.user = result;
        console.log(result);
      },error=>{
        console.log('Something went wrong');
      })
    }

  }

  selectTab(tabId: number) {
    this.staticTabs.tabs[tabId].active = true;
  }

}
