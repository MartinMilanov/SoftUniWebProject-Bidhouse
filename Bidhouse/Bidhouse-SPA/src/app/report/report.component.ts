import { Component, OnInit, Input } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { ReportService } from '../_services/report.service';
import { FormGroup, FormControl } from '@angular/forms';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.css']
})
export class ReportComponent implements OnInit {
  
  @Input() postId;
  modalRef: BsModalRef;

  reportForm = new FormGroup({
    reportType: new FormControl(''),
    description: new FormControl('')
  });

  constructor(private modalService: BsModalService,private reportService: ReportService,private alertify:AlertifyService) { }

  ngOnInit() {
  }

  reportPost(){
    let reportFormValues = this.reportForm.value;
    if(reportFormValues.reportType == ""){
      this.alertify.error("You should choose a report type");
    }
    else if(reportFormValues.description == ""){
      this.alertify.error("You should choose a description");
    }
    else{
     this.reportService.reportPost(this.postId,this.reportForm.value.description,this.reportForm.value.reportType)
    .subscribe(result=>{
     this.alertify.success("Your report has been sent !")
      this.modalRef.hide()
     },error=>{
       this.alertify.error(error);
     })
    }
  }
  openReportForm(reportFormTemplate:any){
    this.modalRef = this.modalService.show(reportFormTemplate);
  }
}
