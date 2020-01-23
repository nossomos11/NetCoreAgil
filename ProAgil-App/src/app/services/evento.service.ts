import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Evento } from 'app/models/Evento';
import { environment } from "../../environments/environment";

@Injectable()

export class EventoService {

    constructor( private http: HttpClient) { }

    baseURL: string = `${environment.baseApiURL}/api/evento`;

    getEventos() : Observable<Evento[]>{
        return this.http.get<Evento[]>(this.baseURL, { headers: this.getTokenHeader()} );
    }

    getEventosByTema(tema: string) : Observable<Evento[]>{
        return this.http.get<Evento[]>(`${this.baseURL}/getByTema/${tema}`, { headers: this.getTokenHeader()});
    }

    getEventosById(id: number) : Observable<Evento[]>{
        return this.http.get<Evento[]>(`${this.baseURL}/${id}`, { headers: this.getTokenHeader()});
    }

    postEvento(evento: Evento){
        return this.http.post(this.baseURL, evento, { headers: this.getTokenHeader()});
    }

    putEvento(evento: Evento){
        return this.http.put(`${this.baseURL}/${evento.ID}`, evento, { headers: this.getTokenHeader()});
    }

    deleteEvento(eventoID: number){
        return this.http.delete(`${this.baseURL}/${eventoID}`, { headers: this.getTokenHeader()});
    }

    postUpload(File: File, nameFileUpload: string){
        const fileToUpload = <File>File[0];
        const formData = new FormData();
        formData.append('file', fileToUpload, nameFileUpload);

        return this.http.post(`${this.baseURL}/upload`, formData, { headers: this.getTokenHeader()});
    }

    getTokenHeader(){
        return new HttpHeaders({'Authorization':`Bearer ${localStorage.getItem('token')}`});
    }
}
