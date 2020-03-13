import { Component, OnInit, Input } from '@angular/core';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';
import { ChangePasswordInputModel } from 'src/viewModels/ChangePasswordInputModel';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {

  @Input() user:any;

   model:any={};

  constructor(private alertify:AlertifyService,private authService:AuthService) { }

  ngOnInit() {
  }

  changePassword(obj:any){
    if(this.model.newPassword != this.model.confirmPassword){
      this.alertify.error("Passwords must match ! ");
      
    }
    if(this.model.newPassword.length < 8 || this.model.newPassword.length > 20)
    {
      this.alertify.error("Password must be longer than 4 and shorter than 8");
    }
    else{
      let credentials = new ChangePasswordInputModel(this.model.currentPassword,this.model.confirmPassword,this.model.newPassword)
      this.authService.changePassword(this.user.id,credentials).subscribe((result)=>{
        this.alertify.success("You have changed your password !");
       
      },error=>{
        console.log(error);
      });
    }
  }
}
