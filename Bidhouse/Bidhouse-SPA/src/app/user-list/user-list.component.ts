import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { UserService } from '../_services/user.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  userList:any[];
  startAt = 0;
  count = 6;
  finished = false;
  loader=false;
  arrayToAdd;

  constructor(private userService:UserService,private alertify:AlertifyService,private changeDetection: ChangeDetectorRef) { }



  ngOnInit() {
    
    this.userService.getUsers(this.startAt,this.count).subscribe(result=>{
    
      this.userList = result as Array<Object>
    },error=>{
      this.finished = true;

    })
    
  }



  onScroll(){
    if(this.finished == false){
      this.loader = true;
      this.finished = true;

      this.startAt+=6;

      this.userService.getUsers(this.startAt,this.count).subscribe(result =>{
        
        if (result == null) {
          this.loader = false;
          this.alertify.message('reached the end of the line there buddy!')
        }
        else{

         // this.arrayToAdd = result as Array<Object>;
          this.loader = false;

          this.userList = this.userList.concat(result as Array<Object>);
          console.log(this.userList);
          this.finished = false;
        }
      },error=>{
        this.finished = true;
        this.alertify.error('nope!')
      })

    }
    // .subscribe(result=>{
      //   this.userList.concat(result as Array<Object>);
      // },error=>{
        //   this.alertify.error("error");
        // });
        // this.userList.concat(this.arrayToAdd);
        // this.changeDetection.detectChanges();
    
  }
  
}
