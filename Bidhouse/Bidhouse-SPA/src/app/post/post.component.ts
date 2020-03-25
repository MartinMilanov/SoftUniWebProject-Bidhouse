import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PostService } from '../_services/post.service';
import { AuthService } from '../_services/auth.service';
import { FormGroup, FormControl } from '@angular/forms';
import { BidInputModel } from 'src/viewModels/BidInputModel';
import { BidService } from '../_services/bid.service';
import { AlertifyService } from '../_services/alertify.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit {
  modalRef: BsModalRef;
 
  approvedBid:any;

  post:any;
  postId:any;

  canBid:boolean
  isCreator:boolean = false;

  bidForm = new FormGroup({
    days: new FormControl(''),
    price: new FormControl(''),
    description: new FormControl('')
  });

  constructor(private route:ActivatedRoute,private postService:PostService,
              private authService:AuthService,private bidService:BidService,
              private alertify:AlertifyService,private modalService: BsModalService) { }

  ngOnInit() {
    this.postId = this.route.snapshot.paramMap.get('id');
    let userId = this.authService.normalizedToken.nameid;

    this.postService.getPost(this.postId).subscribe(result=>{
      this.post = result;
      this.isCreator = this.post.creator.id == userId ? true : false;

      if(this.post.bids != null){

        this.approvedBid = this.post.bids.filter(x=>x.status == 'Approved')[0];
      

      if(this.isCreator == false){
        this.canBid  = this.post.bids.find(x=>x.bidder.id == userId) ? false : true;
        
      }
    }
    else{
      if(this.isCreator == false){
       
        this.canBid = true;
      }
    }

    })
    
    
  }

  placeBid(){

    if(this.bidForm.value.days <= 0){
        this.alertify.error("You should add days !")
    }
    if(this.bidForm.value.price < 0 ){
        this.alertify.error("Price value should be a real price");
    }
    if(this.bidForm.value.description.length <= 10){
      this.alertify.error("Description must be between 10 and 240 characters");
    }
    else{

      let input = new BidInputModel(this.bidForm.value.days,this.bidForm.value.price,this.bidForm.value.description,this.postId,this.post.creator.id);
  
        this.bidService.placeBid(input).subscribe(result=>{
          if(this.post.bids !=null){

            (this.post.bids as Array<any>).push(result);
          }
          else{
            this.post.bids = new Array<any>();
            (this.post.bids as Array<any>).push(result);
          }
          this.canBid = false;
        },error=>{
          console.log(error);
        });
        
        
    }


  }

  openModal(template:any){
    if(this.isCreator == true){
      this.modalRef = this.modalService.show(template);
    }
  }

  approveBid(bidId:string){
    this.bidService.approveBid(bidId,this.postId).subscribe(result=>{
      this.post.bids.find(x=>x.id == bidId).status="Approved";
      this.approvedBid = this.post.bids.find(x=>x.id == bidId);
       this.post.status = "Closed";
      this.modalRef.hide();
    })
  }

  
}
