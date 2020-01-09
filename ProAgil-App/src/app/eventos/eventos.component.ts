import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
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

  eventosFiltrados: any = [];
  eventos: any = [];
  imagemLargura: number = 20;
  mostrarImagem: boolean = true;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getEventos();
  }

  getEventos(){
    this.http.get('http://localhost:5000/api/values/').subscribe(
      response => {
        this.eventos = response;
        this.eventosFiltrados = this.eventos;
      }, error => {
        console.log(error);
      }
    );
  }

  exibirImagem(){
    this.mostrarImagem = ! this.mostrarImagem;
  }

  filtrarLista(filtrarPor: string): any {
    filtrarPor = filtrarPor.toLowerCase();
    console.log(filtrarPor);
    return this.eventos.filter(
      evento => evento.Tema.toLowerCase().indexOf(filtrarPor) !== -1
    );
  }

}
