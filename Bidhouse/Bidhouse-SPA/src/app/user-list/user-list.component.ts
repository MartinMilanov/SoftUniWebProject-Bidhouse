import { Component, OnInit } from '@angular/core';
import { UserService } from '../_services/user.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  userList:{};
  currentPage = 1;


  constructor(private userService:UserService,private alertify:AlertifyService) { }



  ngOnInit() {
    
    this.userService.getUsers().subscribe((result)=>{
      
      this.userList = result as Array<Object>
    
    },error=>{
      this.alertify.error("Could not get users");
    });

    
  }

  


  
}
