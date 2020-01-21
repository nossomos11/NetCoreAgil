import { Component, OnInit, TemplateRef } from '@angular/core';
import { EventoService } from '../services/evento.service';
import { Evento } from '../models/Evento';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css'],
  providers: [EventoService]
})

export class EventosComponent implements OnInit {
  eventosFiltrados: Evento[];
  eventos: Evento[];
  imagemLargura: number = 20;
  mostrarImagem: boolean = true;
  modalRef: BsModalRef;
  registerForm: FormGroup;

  _filtroLista: string = '';

  constructor(
    private eventoService: EventoService,
    private modalService: BsModalService ) 
    { }

  public get filtroLista() : string {
    return this._filtroLista;
  }
  public set filtroLista(value : string) {
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista.length > 0 ? this.filtrarLista(this._filtroLista) : this.eventos;
  }
  
  ngOnInit() {
    this.getEventos();
    this.validation();
  }

  openModal(template: TemplateRef<any>){
    this.modalRef = this.modalService.show(template);
  }
 
  getEventos(){
    this.eventoService.getEventos().subscribe(
      (retornoEventos: Evento[]) => {
        debugger
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
    debugger;
    filtrarPor = filtrarPor != null ? filtrarPor.toLowerCase() : '';
    console.log(filtrarPor);
    return this.eventos.filter(
      evento => evento.Tema.toLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  salvarAlteracao(){

  }

  validation(){
    this.registerForm = new FormGroup({
      Tema: new FormControl('', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]),
      Local: new FormControl('', [Validators.required]),
      DataEvento: new FormControl('', [Validators.required]),
      QtdPessoas: new FormControl('', [Validators.required, Validators.max(1000)]),
      ImagemURL: new FormControl('', [Validators.required]),
      Telefone: new FormControl('', [Validators.required, Validators.minLength(14), Validators.maxLength(15)]),
      Email: new FormControl('', [Validators.required, Validators.email])
    });
  }

}
