import { Component, OnInit, TemplateRef } from '@angular/core';
import { EventoService } from '../services/evento.service';
import { Evento } from '../models/Evento';
import { BsModalRef, BsModalService, BsLocaleService, ptBrLocale, defineLocale } from 'ngx-bootstrap';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
defineLocale('pt-br', ptBrLocale);

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
  registerForm: FormGroup;
  mostrarStatusFormBuilder: boolean = false;
  _filtroLista: string = '';

  constructor(
    private eventoService: EventoService,
    private modalService: BsModalService,
    private formBuilder: FormBuilder,
    private localeService: BsLocaleService ) 
    { 
      this.localeService.use('pt-br');
    }

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
    template.show();
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
    filtrarPor = filtrarPor != null ? filtrarPor.toLowerCase() : '';
    console.log(filtrarPor);
    return this.eventos.filter(
      evento => evento.Tema.toLowerCase().indexOf(filtrarPor) !== -1
    );
  }

  salvarAlteracao(){

  }

  validation(){
    this.registerForm = this.formBuilder.group({
      Tema: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      Local: ['', [Validators.required]],
      DataEvento: ['', [Validators.required]],
      QtdPessoas: ['', [Validators.required, Validators.max(1000)]],
      ImagemURL: ['', [Validators.required]],
      Telefone: ['', [Validators.required, Validators.minLength(14), Validators.maxLength(15)]],
      Email: ['', [Validators.required, Validators.email]]
    });
  }

}
