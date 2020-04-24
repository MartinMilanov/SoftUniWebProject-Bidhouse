import { Component, OnInit, Input } from '@angular/core';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';
import { PostService } from '../_services/post.service';
import { FormGroup, FormControl } from '@angular/forms';
import { AuthService } from '../_services/auth.service';
import { UpdatePostInputModel } from 'src/viewModels/UpdatePostInputModel';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-update-post',
  templateUrl: './update-post.component.html',
  styleUrls: ['./update-post.component.css']
})
export class UpdatePostComponent implements OnInit {

  @Input() postId;

  modalRef: BsModalRef;

  updateForm = new FormGroup({
    description:new FormControl(''),
    price:new FormControl(''),
    location:new FormControl(''),
    date:new FormControl(''),
  })
  constructor(private modalService: BsModalService,private postService:PostService,
    private alertify:AlertifyService,private router:Router) { }

  ngOnInit() {
  }

  openUpdateForm(reportFormTemplate:any){
    this.modalRef = this.modalService.show(reportFormTemplate);
  }

  updatePost(){
    let validationErrorCount = 0;
    let formValue = this.updateForm.value;
      if(formValue.date == ""){
        validationErrorCount++;
        this.alertify.error("You must fill out your date !")
      }
      if(formValue.description == ""){
        validationErrorCount++;
        this.alertify.error("You must fill out your description !")
      }
      if(formValue.price == ""){
        validationErrorCount++;
        this.alertify.error("You must fill out your budget !")
      }
      if(formValue.location == ""){
        validationErrorCount++;
        this.alertify.error("You must fill out your location !")
      }
      if(validationErrorCount == 0){
        var input = new UpdatePostInputModel(this.postId,formValue.description,formValue.price,formValue.location,formValue.date);
        
         this.postService.updatePost(input).subscribe(result=>{
           this.alertify.success("You have successfully updated your post !")
           let currentUrl = this.router.url;
    
           this.modalRef.hide();
           this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
            this.router.navigate([currentUrl]);
        });
         },error=>{
          let messages = error.split("\n");
          messages = messages.filter(x=>x.length>1)
          messages.forEach(message => {
            this.alertify.error(message);
          });
         })

      }
  }
}
