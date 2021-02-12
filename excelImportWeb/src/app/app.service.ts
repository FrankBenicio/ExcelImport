
import { Injectable } from '@angular/core';
import { HttpClient,HttpHeaders }    from '@angular/common/http';  
@Injectable({
  providedIn: 'root'
})
export class AppService {  
  readonly rootURL = 'https://localhost:5001/api';
  constructor(private http: HttpClient) { }  
      httpOptions = {  
        headers: new HttpHeaders({  
          'Content-Type': 'multipart/form-data',
          'Accept': 'application/json',
          'Access-Control-Allow-Origin:': '*',
        })  
      }          
      postData(file : any){  
        return this.http.post(this.rootURL + '/Excel/upload',file);  
      }   
}