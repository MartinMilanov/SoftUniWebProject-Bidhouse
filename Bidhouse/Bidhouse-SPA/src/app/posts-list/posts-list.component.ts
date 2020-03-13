import { Component, OnInit, Input, TemplateRef } from '@angular/core';
import { PostService } from '../_services/post.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { Router } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-posts-list',
  templateUrl: './posts-list.component.html',
  styleUrls: ['./posts-list.component.css']
})
export class PostsListComponent implements OnInit {
  posts:any[];
  @Input() id : any;
  
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
}
