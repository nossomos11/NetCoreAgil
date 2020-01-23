import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { User } from "../../models/User";
import { AuthService } from "../../services/auth.service";
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  registerForm: FormGroup;
  user: User;
  

  constructor(private formBuilder: FormBuilder,
              private toastrService: ToastrService,
              private authService:  AuthService,
              private router: Router )
  {

  }

  ngOnInit() {
    this.validation();
  }

  validation()
  {
    this.registerForm = this.formBuilder.group({
      fullName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      userName: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      passwords: this.formBuilder.group({
        password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(30)]],
        confirmPassword: ['', Validators.required]
      }, { validator: this.compararSenhas })
    });
  }

  cadastrarUsuario() {
    if (this.registerForm.valid) {
      this.user = Object.assign({ password: this.registerForm.get('passwords.password').value }, this.registerForm.value);
      this.authService.register(this.user).subscribe(
        () => {
          this.router.navigate(['/user/login']);
          this.toastrService.success('Cadastro Realizado com Sucesso!');
        }, error => {
          const erro = error.error ? error.error : error;
          if(erro.length && erro.length > 0){
            erro.forEach(element => {
              switch (element.Code) {
                case 'DuplicateUserName':
                  this.toastrService.error('Usuário já Cadastrado!');
                  break;
                default:
                  this.toastrService.error(`Erro durante o Cadastro do Usuário! Code: ${element.Code}`);
                  break;
              }
            });
          }else{
            this.toastrService.error(`Erro durante o Cadastro do Usuário! Code: ${erro.status} - ${erro.statusText}`);
          }
        }
      );
    }
  }

  compararSenhas(fb: FormGroup) {
    const confirmSenhaCtrl = fb.get('confirmPassword');
    if (confirmSenhaCtrl.errors == null || 'mismatch' in confirmSenhaCtrl.errors) {
      if (fb.get('password').value !== confirmSenhaCtrl.value) {
        confirmSenhaCtrl.setErrors({ mismatch: true });
      } else {
        confirmSenhaCtrl.setErrors(null);
      }
    }
  }  

}
