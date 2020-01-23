import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'environments/environment';
import { map } from "rxjs/operators";
import { JwtHelperService } from "@auth0/angular-jwt";

@Injectable({
  providedIn: 'root'
})

export class AuthService {

  constructor( private http: HttpClient) { }

  baseURL: string = `${environment.baseApiURL}/api/user`;
  jwtHelper = new JwtHelperService();
  decodedToken: any;

  register(model: any){
    return this.http.post(`${this.baseURL}/register`, model);
  } 


  login(model: any){
    return this.http.post(`${this.baseURL}/login`, model).pipe(
      map((response: any) =>{
        const user = response;
        if(user){
          localStorage.setItem('token', user.token);
          localStorage.setItem('username', user.user.UserName);
          localStorage.setItem('email', user.user.Email);
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
        }
      })
    );
  }

  logout(){
    if(localStorage.getItem('token')){
      localStorage.removeItem('token');
      localStorage.removeItem('username');
      localStorage.removeItem('email');
    }
  }

  loggedIn(){
    const token = localStorage.getItem('token');
    return ! this.jwtHelper.isTokenExpired(token);
  }
}
