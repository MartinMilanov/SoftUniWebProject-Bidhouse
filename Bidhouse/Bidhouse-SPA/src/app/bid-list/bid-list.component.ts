import { Component, OnInit } from '@angular/core';
import { BidService } from '../_services/bid.service';

@Component({
  selector: 'app-bid-list',
  templateUrl: './bid-list.component.html',
  styleUrls: ['./bid-list.component.css']
})
export class BidListComponent implements OnInit {
  
  bids:any[];
  constructor(private bidService:BidService) { }

  ngOnInit() {
    this.bidService.getBids().subscribe(result=>{
      this.bids = result as Array<any>;
    },error=>{
    })
  }

}
