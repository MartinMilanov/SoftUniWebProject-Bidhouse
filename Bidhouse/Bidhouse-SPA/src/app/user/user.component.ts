import { Component, OnInit, TemplateRef} from '@angular/core';
import { UserService } from '../_services/user.service';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { ChangePasswordInputModel } from 'src/viewModels/ChangePasswordInputModel';
import { Router } from '@angular/router';
import { UserUpdateModel } from 'src/viewModels/UserUpdateModel';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';
import { PostService } from '../_services/post.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
user:any;
postsArray:any[];

  myForm = new FormGroup({
    city: new FormControl(''),
    jobPosition:new FormControl(''),
    description:new FormControl(''),
    image: new FormControl(''),
    imageSource: new FormControl('')
  });

  modalRef: BsModalRef;
  preview:string;
  model:any={};

  constructor(private userService:UserService,private authService:AuthService,private alertify:AlertifyService,
    private router:Router,private modalService: BsModalService,private postService:PostService) { 
  }
  
  
  ngOnInit() {
   this.userService.getUser(this.authService.normalizedToken.nameid).subscribe((result)=>{
     this.user = result;
     this.postsArray = this.user.posts as Array<Object>;
     console.log(this.postsArray)
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

  onFileChange(event:any) {
  
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      this.myForm.patchValue({
        imageSource: file
      });

      this.myForm.get('imageSource').updateValueAndValidity()

    // File Preview
    const reader = new FileReader();
    reader.onload = () => {
      this.preview = reader.result as string;
    }
    reader.readAsDataURL(file)
    }
  }

  updateDetails(){
      let input = new UserUpdateModel(this.myForm.get('jobPosition').value,this.myForm.get('city').value,this.myForm.get('description').value);
      const formData = new FormData();
      formData.append('file',this.myForm.get('imageSource').value,this.myForm.get('imageSource').value.name);
      formData.append("JobPosition",input.JobPosition);
      formData.append("City",input.City);
      formData.append("Description",input.Description);

        this.userService.updateUser(this.user.id,input,formData).subscribe((result)=>{
        this.alertify.success("You've successfully updated your profile !");
        this.router.routeReuseStrategy.shouldReuseRoute = () => false;
        this.router.onSameUrlNavigation = 'reload';
        this.router.navigate(['/user']);
      },error=>{
        this.alertify.error('Error!');
      })



   
  }


  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }
  
  confirm(post:any): void {
    this.postsArray.splice(this.postsArray.indexOf(post),1);
    this.postService.deletePost(post.id).subscribe(result=>{

      this.alertify.success('You have deleted '+ post.name);
    },error=>{
      this.alertify.error("Something went wrong, please alert support !")
    }
    );
    this.modalRef.hide();
  }
 
  decline(): void {
    this.modalRef.hide();
  }
}
