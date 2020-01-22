import { Lote } from "./Lote";
import { RedeSocial } from "./RedeSocial";
import { Palestrante } from "./Palestrante";

export class Evento {

    constructor(){}

    ID: number; 
    Local: string; 
    DataEvento: Date; 
    Tema: string; 
    QtdPessoas: number; 
    ImagemURL: string; 
    Telefone: string; 
    Email: string; 
    Lotes: Lote[]; 
    RedeSociais: RedeSocial[]; 
    PalestrantesEventos: Palestrante[];
}
