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
    
      let credentials = new ChangePasswordInputModel(this.model.currentPassword,this.model.confirmPassword,this.model.newPassword)
      this.authService.changePassword(this.user.id,credentials).subscribe((result)=>{
        this.alertify.success("You have changed your password !");
       
      },error=>{
        let messages = error.split("\n");
        messages = messages.filter(x=>x != "");

        messages.forEach(message => {
          this.alertify.error(message);
        });
      });
    }
}

