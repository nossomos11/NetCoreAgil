import { RedeSocial } from "./RedeSocial";
import { Evento } from "./Evento";

export class Palestrante {

    constructor(){}

    ID: number; 
    Nome: string;
    Miniurriculo: string;
    ImagemURL: string;
    Telefone: string;
    Email: string;
    RedeSociais: RedeSocial[];
    PalestrantesEventos: Evento[];
}
