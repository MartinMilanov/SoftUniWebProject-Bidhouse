import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { UserService } from '../_services/user.service';
import { AlertifyService } from '../_services/alertify.service';
import { GetUserQueryInput } from 'src/viewModels/GetUserQueryInput';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
 
  query = new GetUserQueryInput(0,6,null);
  userList:any[];
  finished = false;
  loader=false;
  searchActive = false;
  arrayToAdd;

  constructor(private userService:UserService,private alertify:AlertifyService,private changeDetection: ChangeDetectorRef) { }



  ngOnInit() {
    this.loader = true;
    this.userService.getUsers(this.query).subscribe(result=>{
      
      this.userList = result as Array<Object>
      this.loader = false;
    },error=>{
      this.finished = true;

    })
    
  }

  addFilter(event:any){
    this.query.SearchInput = event.value;
    this.query.StartAt = 0;
    this.query.Count = 6;
    
    this.loader = true;
    this.finished = false;
    this.searchActive = true;
    this.userService.getUsers(this.query).subscribe(result=>{
        this.loader = false;
        this.userList = result as Array<Object>;
        
     })

  }

  onScroll(){
    if(this.finished == false){
      this.loader = true;
      this.alertify.message("Loading...");
      this.finished = true;

      this.query.StartAt+=6;

      this.userService.getUsers(this.query).subscribe(result =>{
        
        if (result == null) {
          this.loader = false;
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
