import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { HttpClientModule  } from '@angular/common/http';

import { AppComponent } from './app.component';
import { EventosComponent } from './eventos/eventos.component';
import { NavComponent } from "./nav/nav.component";
import { EventoService } from './services/evento.service';

@NgModule({
   declarations: [
      AppComponent,
      EventosComponent,
      NavComponent
   ],
   imports: [
      BrowserModule,
      FormsModule,
      HttpModule,
      HttpClientModule,
      FormsModule
   ],
   providers: [
     // EventoService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
