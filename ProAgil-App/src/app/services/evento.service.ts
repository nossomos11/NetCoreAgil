import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()

export class EventoService {

    constructor( private http: HttpClient) { }

    baseURL: string = "http://localhost:5000/api/evento"; 

    getEventos(){
        return this.http.get(this.baseURL);
    }

}
