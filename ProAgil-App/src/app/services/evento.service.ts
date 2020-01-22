import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Evento } from 'app/models/Evento';
import { environment } from "../../environments/environment";

@Injectable()

export class EventoService {

    constructor( private http: HttpClient) { }

    baseURL: string = `${environment.baseApiURL}/api/evento`;

    getEventos() : Observable<Evento[]>{
        return this.http.get<Evento[]>(this.baseURL);
    }

    getEventosByTema(tema: string) : Observable<Evento[]>{
        return this.http.get<Evento[]>(`${this.baseURL}/getByTema/${tema}`);
    }

    getEventosById(id: number) : Observable<Evento[]>{
        return this.http.get<Evento[]>(`${this.baseURL}/${id}`);
    }

    postEvento(evento: Evento){
        return this.http.post(this.baseURL, evento);
    }

    putEvento(evento: Evento){
        return this.http.put(`${this.baseURL}/${evento.ID}`, evento);
    }

    deleteEvento(eventoID: number){
        return this.http.delete(`${this.baseURL}/${eventoID}`);
    }
}
