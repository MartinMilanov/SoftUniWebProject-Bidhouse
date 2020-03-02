import { Component, OnInit } from '@angular/core';
import { UserService } from '../_services/user.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-user-guest',
  templateUrl: './user-guest.component.html',
  styleUrls: ['./user-guest.component.css']
})
export class UserGuestComponent implements OnInit {
  user:any;
  constructor(private userService:UserService,private route:ActivatedRoute) { }

  ngOnInit() {
    let id = this.route.snapshot.paramMap.get('id');
    this.userService.getUser(id).subscribe(result=>{
      this.user = result;
      console.log(this.user);
    },error=>{
      console.log('oops');
    })

  }

}
