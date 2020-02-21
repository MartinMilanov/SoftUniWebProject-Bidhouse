import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
 
  @Output() declineRegister = new EventEmitter();

  model:any={};


  constructor(private authService:AuthService) { }

  ngOnInit() {
  }

  register(){
    this.authService.register(this.model).subscribe((result)=>{
      console.log('register in successfull')
    },error=>{
      console.log(error)
    })
  }

  cancel(){
    this.declineRegister.emit(true);
  }
}
