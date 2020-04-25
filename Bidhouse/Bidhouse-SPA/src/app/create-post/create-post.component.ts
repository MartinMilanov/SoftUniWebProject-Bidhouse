import { Component, OnInit } from '@angular/core';
import { AlertifyService } from '../_services/alertify.service';
import { FormGroup, FormControl } from '@angular/forms';
import { CreatePostInputModel } from 'src/viewModels/CreatePostInputModel';
import { PostService } from '../_services/post.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-post',
  templateUrl: './create-post.component.html',
  styleUrls: ['./create-post.component.css']
})
export class CreatePostComponent implements OnInit {

  form = new FormGroup({
    name: new FormControl(''),
    description: new FormControl(''),
    category:new FormControl(''),
    location:new FormControl(''),
    time:new FormControl(''),
    price:new FormControl('')

  })

  constructor(private alertify:AlertifyService,private postService:PostService,private router:Router) { }

  ngOnInit() {
  }

  createPost(){
    if (this.form.value.name.length < 10 || this.form.value.name.length >70 ) {
      this.alertify.error("Your post title must be between 10 and 70 characters")
    }
    if (this.form.value.description.length < 10 || this.form.value.description.length > 270) {
      this.alertify.error("Your description must be between 10 and 270 characters")
    }
    if (this.form.value.price <= 0) {
      this.alertify.error("You must add a price");
    }
    if (this.form.value.time == null || this.form.value.time <= Date.now()){
      this.alertify.error("You must add days to your inqury");
    }
    else{

      let input = new CreatePostInputModel(this.form.value.name,this.form.value.description,this.form.value.category,this.form.value.location,this.form.value.time,this.form.value.price);
      console.log(input);
      this.postService.createPost(input).subscribe(result=>{
        this.alertify.success("Congratulations, you have successfully created your post !");
        this.router.routeReuseStrategy.shouldReuseRoute = () => false;
        this.router.onSameUrlNavigation = 'reload';
        this.router.navigate(['/user']);
      },error=>{
        console.log(error);
        this.alertify.error("Something went wrong , please contact support for further help");
      });
  


    }
  }
}
