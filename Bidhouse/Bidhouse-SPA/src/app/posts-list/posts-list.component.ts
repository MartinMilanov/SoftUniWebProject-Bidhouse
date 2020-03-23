import { Component, OnInit, Input, TemplateRef } from '@angular/core';
import { PostService } from '../_services/post.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { Router } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-posts-list',
  templateUrl: './posts-list.component.html',
  styleUrls: ['./posts-list.component.css']
})
export class PostsListComponent implements OnInit {
  
  @Input() id : any;
  @Input() isGuestUser:boolean=false;
  posts:any[];
  
  filters = new FormGroup({
    searchInput: new FormControl(''),
    category:new FormControl('')
  })

  finished :boolean = false;
  loader:boolean = false;

  searchInput = "";
  category = "";
  skipCount:number = 0;
  takeCount:number = 5;


  modalRef: BsModalRef;
  

  
  constructor(private postService:PostService,private modalService: BsModalService,
    private router:Router,private alertify:AlertifyService) { }

  ngOnInit() {
    if(this.id != null && this.id != ""){
      this.postService.getPostsById(this.id).subscribe(result=>{
        this.posts = result as Array<any>
      },error => {
        console.log(error);
      })
    }
    else{
      this.postService.getPosts("","",0,5).subscribe(result=>{
        this.posts = result as Array<any>;
      })
    }
  }



  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(post:any): void {
    this.posts.splice(this.posts.indexOf(post),1);
    this.postService.deletePost(post.id).subscribe(result=>{

      this.alertify.success('You have deleted '+ post.name);
      this.router.routeReuseStrategy.shouldReuseRoute = () => false;
      this.router.onSameUrlNavigation = 'reload';
      this.router.navigate(['/user']);
    },error=>{
      this.alertify.error("Something went wrong, please alert support !")
    }
    );
    this.modalRef.hide();
  }
 
  decline(): void {
    this.modalRef.hide();
  }

  onScroll(){
    if(this.finished == false){
      this.loader = true;
      this.alertify.message("Loading...");
      this.finished = true;

      this.skipCount+=5;

      this.postService.getPosts(this.category,this.searchInput,this.skipCount,this.takeCount).subscribe(result =>{
        
        if (result == null) {
          this.loader = false;
        }
        if ((result as Array<any>).length <= 0 ) {
          this.finished = true;
          console.log("finished");
        }
        else{

          this.loader = false;

          this.posts = this.posts.concat(result as Array<any>);
          console.log(this.posts);
          this.finished = false;
        }
      },error=>{
        this.finished = true;
        this.alertify.error('Something went wrong!')
      })

    }
    
  }

  filter(){
   this.category =  this.filters.value.category;
   this.searchInput = this.filters.value.searchInput;
  
    this.postService.getPosts(this.category,this.searchInput,0,5).subscribe(result=>{
      this.posts = result as Array<any>
     this.finished = false;
  })
  }

}
