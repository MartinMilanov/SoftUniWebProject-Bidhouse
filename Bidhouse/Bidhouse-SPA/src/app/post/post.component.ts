import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PostService } from '../_services/post.service';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit {
  post:any;

  constructor(private route:ActivatedRoute,private postService:PostService) { }

  ngOnInit() {
    let id = this.route.snapshot.paramMap.get('id');
    this.postService.getPost(id).subscribe(result=>{
      this.post = result;
    })
  }
 
}
