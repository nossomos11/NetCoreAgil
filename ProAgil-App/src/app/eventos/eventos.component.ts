import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {

  eventos: any = [
    {
      EventoID: 1,
      Tema: 'NetCore',
      Local: "Belo Horizonte"
    },
    {
      EventoID: 2,
      Tema: 'Angular',
      Local: "São Paulo"
    },
    {
      EventoID: 3,
      Tema: 'NetCore e Angular',
      Local: "Rio de Janeiro"
    }
];

  constructor() { }

  ngOnInit() {
  }

}
