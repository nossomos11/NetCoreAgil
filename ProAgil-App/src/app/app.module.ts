import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule, TooltipModule, ModalModule, BsDatepickerModule } from 'ngx-bootstrap';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { AppRoutingModule } from "../app/app-routing.module";

import { EventoService } from './services/evento.service';

import { AppComponent } from './app.component';
import { NavComponent } from "./nav/nav.component";
import { EventosComponent } from './eventos/eventos.component';
import { PalestrantesComponent } from './palestrantes/palestrantes.component';
import { ContatosComponent } from './contatos/contatos.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { TituloComponent } from './shared/titulo/titulo.component';

import { DateTimeFormatPipe } from './helps/DateTimeFormatPipe.pipe';

@NgModule({
   declarations: [
      AppComponent,
      EventosComponent,
      NavComponent,
      DateTimeFormatPipe,
      PalestrantesComponent,
      DashboardComponent,
      ContatosComponent,
      TituloComponent
   ],
   imports: [
      BrowserModule,
      BrowserAnimationsModule,
      FormsModule,
      HttpClientModule,
      FormsModule,
      BsDropdownModule.forRoot(),
      BsDatepickerModule.forRoot(),
      TooltipModule.forRoot(),
      ModalModule.forRoot(),
      ReactiveFormsModule,
      ToastrModule.forRoot(),
      AppRoutingModule
   ],
   providers: [
      //EventoService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
