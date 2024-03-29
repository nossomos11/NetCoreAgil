import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'app/services/auth.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})

export class NavComponent implements OnInit {

  constructor(public router: Router,
    private toastr: ToastrService,
    private authService: AuthService) { }

  ngOnInit() {
    if(! this.loggedIn() && this.router.url !== '/user/login' && this.router.url !== '/user/register'){
      this.entrar();
    }
  }

  showMenu() {
    return this.router.url !== '/user/login';
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  entrar() {
    this.router.navigate(['/user/login']);
  }

  logout() {
    this.authService.logout();
    this.toastr.show('Log Out');
    this.router.navigate(['/user/login']);
  }

  userName() {
    return localStorage.getItem('username');
  }
}