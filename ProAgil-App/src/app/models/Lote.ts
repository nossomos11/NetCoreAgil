export interface Lote {
    ID: number;
    Nome: string;
    Preco: number;
    DataInicio: Date;
    DataFim: Date;
    Quantidade: number;
    EventoID?: number;
}
