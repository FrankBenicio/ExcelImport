import { Component } from '@angular/core';
import {AppService} from './app.service';  
import { FormGroup, FormControl,Validators } from '@angular/forms';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(private AppService: AppService) {
   }  
  data: any;  

  onUpload(event: any) {  
    console.log("entrei aki");
    const file = event.target.files[0];
    const formData = new FormData();

    formData.append("file", file);

    this.AppService.postData(formData).subscribe((data) => {  
      this.data = data;    
    })  
  }   
}