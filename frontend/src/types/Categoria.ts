export enum FinalidadeCategoria {
    Despesa = 1,
    Receita = 2,
    Ambas = 3,
}

export interface Categoria {
    id: string;
    descricao: string;
    finalidade: FinalidadeCategoria;
}
