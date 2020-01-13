import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { EventoService } from 'app/services/evento.service';
import { Evento } from 'app/models/Evento';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css'],
  providers: [EventoService]
})

export class EventosComponent implements OnInit {

  _filtroLista: string = '';

  public get filtroLista() : string {
    return this._filtroLista;
  }
  public set filtroLista(value : string) {
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista.length > 0 ? this.filtrarLista(this._filtroLista) : this.eventos;
  }

  eventosFiltrados: Evento[];
  eventos: Evento[];
  imagemLargura: number = 20;
  mostrarImagem: boolean = true;

  constructor(private eventoService: EventoService) { }

  ngOnInit() {
    this.getEventos();
  }

  getEventos(){
    this.eventoService.getEventos().subscribe(
      (retornoEventos: Evento[]) => {
        this.eventos = retornoEventos;
        this.eventosFiltrados = this.eventos;
        console.log(retornoEventos);
      }, error => {
        console.log(error);
      }
    );
  }

  exibirImagem(){
    this.mostrarImagem = ! this.mostrarImagem;
  }

  filtrarLista(filtrarPor: string): Evento[] {
    filtrarPor = filtrarPor.toLowerCase();
    console.log(filtrarPor);
    return this.eventos.filter(
      evento => evento.Tema.toLowerCase().indexOf(filtrarPor) !== -1
    );
  }

}
