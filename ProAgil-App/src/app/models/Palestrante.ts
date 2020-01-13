import { RedeSocial } from "./RedeSocial";
import { Evento } from "./Evento";

export interface Palestrante {
    ID: number; 
    Nome: string;
    Miniurriculo: string;
    ImagemURL: string;
    Telefone: string;
    Email: string;
    RedeSociais: RedeSocial[];
    PalestrantesEventos: Evento[];
}
