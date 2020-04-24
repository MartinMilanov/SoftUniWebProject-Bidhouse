import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
 
  @Output() declineRegister = new EventEmitter();

  model:any={};


  constructor(private authService:AuthService,private alertify:AlertifyService) { }

  ngOnInit() {
  }

  register(){
    this.authService.register(this.model).subscribe((result)=>{
      this.alertify.success("You have successfully registered !");
    },error=>{
        let messages = error.split("\n");
        messages = messages.filter(x=>x != "");
        messages.forEach(message => {
          this.alertify.error(message);
        });
     
    })
  }

  cancel(){
    this.declineRegister.emit(true);
  }
}
