import { NgModule } from '@angular/core';
import { Routes, RouterModule } from "@angular/router";
import { EventosComponent } from './eventos/eventos.component';
import { PalestrantesComponent } from './palestrantes/palestrantes.component';
import { ContatosComponent } from './contatos/contatos.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { UserComponent } from './user/user.component';
import { LoginComponent } from './user/login/login.component';
import { RegistrationComponent } from './user/registration/registration.component';

const routes: Routes = [
  { path:'user', component: UserComponent,
    children:[
      { path:'login', component: LoginComponent },
      { path:'register', component: RegistrationComponent }

  ]},
  { path:'cadastro', component: EventosComponent },
  { path:'palestrantes', component: PalestrantesComponent },
  { path:'contatos', component: ContatosComponent },
  { path:'dashboard', component: DashboardComponent },
  { path:'', redirectTo: '/dashboard', pathMatch: 'full' },
  { path:'**', redirectTo: '/dashboard', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {onSameUrlNavigation: 'reload'} )],
  exports: [RouterModule]
})
export class AppRoutingModule { }
