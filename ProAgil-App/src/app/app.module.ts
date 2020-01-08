import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { HttpClientModule  } from '@angular/common/http';

import { AppComponent } from './app.component';
import { EventosComponent } from './eventos/eventos.component';

@NgModule({
   declarations: [
      AppComponent,
      EventosComponent
   ],
   imports: [
      BrowserModule,
      FormsModule,
      HttpModule,
      HttpClientModule
   ],
   providers: [],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
