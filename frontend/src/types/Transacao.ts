export enum TipoTransacao {
    Despesa = 1,
    Receita = 2,
}

export interface Transacao {
    id: string;
    descricao: string;
    valor: number;
    tipo: TipoTransacao;
    pessoaId: string;
    categoriaId: string;
}
