import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {

  titulo: 'Login';
  model: any = {};

  constructor(private toastrService: ToastrService,
    private authService:  AuthService,
    private router: Router ){
      
    }

  ngOnInit() {
    if (this.authService.loggedIn()) {
      this.router.navigate(['/dashboard']);
    }
  }

  login(){
    this.authService.login(this.model)
      .subscribe(
        () => {
          this.router.navigate(['/dashboard']);
          this.toastrService.success('Usuário Logado com Sucesso');
        },
        error => {
          switch (error.status) {
            case 0:
              this.toastrService.error(`Erro ao tentar Logar! Serviço Indisponível!`);
              break;
            case 401:
              this.toastrService.warning(`Usuário ou senha inválido`);
              break;
            default:
              this.toastrService.error(`Erro ao tentar Logar! Erro: (${error.status}) ${error.statusText}`);
              break;
          }
        }
      );

  }

  registrar(){
    this.router.navigate(['/user/register']);
  }

}
