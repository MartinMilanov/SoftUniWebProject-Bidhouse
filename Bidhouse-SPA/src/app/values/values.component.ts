import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHandler, HttpHeaders } from '@angular/common/http';
@Component({
  selector: 'app-values',
  templateUrl: './values.component.html',
  styleUrls: ['./values.component.css']
})
export class ValuesComponent implements OnInit {

  constructor(private http:HttpClient) { }

  ngOnInit() {
    this.http.get("https://localhost:44394/api/values").subscribe((response)=>{
      console.log(response)
    },error=>{
      console.log(error);
    }
      
    )
  }

}
