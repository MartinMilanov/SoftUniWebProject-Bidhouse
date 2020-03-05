import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PostService } from '../_services/post.service';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit {
  post:any;
  userId;
  constructor(private route:ActivatedRoute,private postService:PostService,private authService:AuthService) { }

  ngOnInit() {
    let id = this.route.snapshot.paramMap.get('id');
    this.postService.getPost(id).subscribe(result=>{
      this.post = result;
    })

    this.userId = this.authService.normalizedToken.nameid;
  }
 
}
