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
  eventoSelecionado: Evento;
  imagemLargura: number = 20;
  mostrarImagem: boolean = true;
  registerForm: FormGroup;
  mostrarStatusFormBuilder: boolean = false;
  mensagemDeletarEvento: string = '';
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

  openModal(template: any){
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

  novoEvento(template: any){
    this.eventoSelecionado = null;
    this.registerForm.reset();
    this.openModal(template);
  }

  editarEvento(evento: Evento, template: any){
    this.eventoSelecionado = evento;
    this.registerForm.patchValue(this.eventoSelecionado);
    this.openModal(template);
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

  salvarAlteracao(template: any){
    if(this.registerForm.valid){
      if(this.eventoSelecionado == null){
        this.eventoSelecionado = Object.assign({}, this.registerForm.value);
        console.log(this.eventoSelecionado);
        this.eventoService.postEvento(this.eventoSelecionado).subscribe(
          (eventoCriado: Evento) => {
            console.log(eventoCriado);
            template.hide();
            this.getEventos();
          }, error => {
            console.log(error);
          }
        );
      }else{
        this.eventoSelecionado = Object.assign({ID: this.eventoSelecionado.ID}, this.registerForm.value);
        this.eventoService.putEvento(this.eventoSelecionado).subscribe(
          (eventoAtualizado: Evento) => {
            console.log(eventoAtualizado);
            template.hide();
            this.getEventos();
          }, error =>{
            console.log(error);
          }
        );
      }
    }
  }

 
  excluirEvento(evento: Evento, template: any) {
    this.eventoSelecionado = evento;
    this.mensagemDeletarEvento = `Tem certeza que deseja excluir o Evento: ${this.eventoSelecionado.Tema}, Código: ${this.eventoSelecionado.ID}`;
    this.openModal(template);
  }
 
  confirmExcluirEvento(template: any) {
    this.eventoService.deleteEvento(this.eventoSelecionado.ID).subscribe(
      () => {
          template.hide();
          this.getEventos();
        }, error => {
          console.log(error);
        }
    );
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
