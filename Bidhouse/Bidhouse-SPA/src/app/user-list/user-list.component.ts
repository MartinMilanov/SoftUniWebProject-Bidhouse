import { Component, OnInit, ChangeDetectorRef, Input } from '@angular/core';
import { UserService } from '../_services/user.service';
import { AlertifyService } from '../_services/alertify.service';
import { GetUserQueryInput } from 'src/viewModels/GetUserQueryInput';
import { AdminService } from '../_services/admin.service';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  @Input() adminMode : boolean = false;
  query = new GetUserQueryInput(0,6,"");
  userList:any[];
  finished = false;
  loader=false;
  searchActive = false;
  arrayToAdd;

  constructor(private userService:UserService,private alertify:AlertifyService,
    private changeDetection: ChangeDetectorRef, private adminService:AdminService,public authService:AuthService) { }



  ngOnInit() {
    this.loader = true;
    this.userService.getUsers(this.query).subscribe(result=>{
      
      this.userList = result as Array<Object>;
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
          this.loader = false;

          this.userList = this.userList.concat(result as Array<Object>);
          this.finished = false;
        }
      },error=>{
        this.finished = true;
        this.alertify.error('nope!')
      })

    }
    
  }
  
  deleteUser(id:string){
    this.adminService.deleteUser(id).subscribe(result=>{
      this.alertify.success("You have successfully deleted the user ! ")
      this.userList = this.userList.filter(x=>x.id != id);
    })

  }

  makeAdmin(id:string){
    this.adminService.makeAdmin(id).subscribe(result=>{
      this.userList.find(x=>x.id == id).role = "Admin";
      this.alertify.success("This user is now an admin !");
    },error=>{
      this.alertify.error(error);
    })
  }
}
