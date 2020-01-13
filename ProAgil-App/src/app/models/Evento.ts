import { Lote } from "./Lote";
import { RedeSocial } from "./RedeSocial";
import { Palestrante } from "./Palestrante";

export interface Evento {
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
