import { Component, OnInit } from '@angular/core';
import { ReportService } from '../_services/report.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap';
import { PostService } from '../_services/post.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-report-list',
  templateUrl: './report-list.component.html',
  styleUrls: ['./report-list.component.css']
})
export class ReportListComponent implements OnInit {
  reportsList:any[];
  modalRef: BsModalRef;
  reportResult;

  constructor(private reportService:ReportService,private modalService: BsModalService,
    private postService:PostService,private alertify:AlertifyService) { }

  ngOnInit() {
    this.reportService.getReports().subscribe(result=>{
      this.reportsList = result as Array<any>;
    })
  }

  openReport(reportDetails:any,id:string){
    this.reportService.getReport(id).subscribe(result=>{
        this.reportResult = result;
        reportDetails.show();
    })

  }

  deletePost(id:string,reportId:string){
    this.postService.deletePost(id).subscribe(result=>{
      this.reportsList = this.reportsList.filter(x=>x.id == reportId);
      this.alertify.success("You have successfully removed this post !")
    })
  }

  deleteReport(id:string){
    this.reportService.deleteReport(id).subscribe(result=>{
      this.reportsList = this.reportsList.filter(x=>x.id == id);
      this.alertify.success("You have successfully deleted this report ");
    })
  }
}
