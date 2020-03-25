import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { RateService } from '../_services/rate.service';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-rate',
  templateUrl: './rate.component.html',
  styleUrls: ['./rate.component.css']
})
export class RateComponent implements OnInit {
  @Input() reviewedId : string
  @Input() reviews: any;

  reviewSummary;

  canReview:boolean = true;

  form = new FormGroup({
     description:new FormControl(''),
     rating: new FormControl('')
   })


  constructor(private rateService:RateService,private authService:AuthService) { }

  ngOnInit() {
    var user = this.reviews.find(x=>x.reviewerId == this.authService.normalizedToken.nameid);
    this.reviewedId == this.authService.normalizedToken.nameid ? this.canReview = false : this.canReview = true;
    this.reviewSummary = (this.reviews as Array<any>).slice(0,3);
    if (user!=null) {
      this.canReview = false;
    }


  }

  writeReview(){
    this.rateService
    .reviewUser(this.reviewedId,this.form.value.description,this.form.value.rating).subscribe((result)=>{
      this.reviews.push(result);
      this.canReview = false;
    },error=>{
      console.log(error);
    })
  }
}
