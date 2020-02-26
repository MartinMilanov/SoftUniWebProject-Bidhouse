import { Component, OnInit} from '@angular/core';
import { UserService } from '../_services/user.service';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { ChangePasswordInputModel } from 'src/viewModels/ChangePasswordInputModel';
import { Router } from '@angular/router';
import { UserUpdateModel } from 'src/viewModels/UserUpdateModel';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  

  model:any={};
  user;

  constructor(private userService:UserService,private authService:AuthService,private alertify:AlertifyService,private router:Router) { 
  }
  
  
  ngOnInit() {
   this.userService.getUser(this.authService.normalizedToken.nameid).subscribe((result)=>{
     this.user = result;
   })
  }

  changePassword(obj:any){
    if(this.model.newPassword != this.model.confirmPassword){
      this.alertify.error("Passwords must match ! ");
    }
    if(this.model.newPassword.length < 4 || this.model.newPassword.length > 8)
    {
      this.alertify.error("Password must be longer than 4 and shorter than 8");
    }
    else{
      let credentials = new ChangePasswordInputModel(this.model.confirmPassword,this.model.newPassword)
      this.authService.changePassword(this.user.id,credentials).subscribe((result)=>{
        this.alertify.success("You have changed your password !");
       
      },error=>{
        this.alertify.error("Something went wrong");
      });
    }
  }

  updateDetails(){
    console.log(this.model);
    let input = new UserUpdateModel(this.model.JobPosition,this.model.City,this.model.Description);
    this.userService.updateUser(this.user.id,input).subscribe((result)=>{
      this.alertify.success("You've upated ! ")
      this.router.routeReuseStrategy.shouldReuseRoute = () => false;
      this.router.onSameUrlNavigation = 'reload';
      this.router.navigate(['/user']);
    },error=>{
      this.alertify.error("Something went wrong !");
    });

  }
}
