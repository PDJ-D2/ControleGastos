import { http } from "./http";
import { Transacao, TipoTransacao } from "../types/Transacao";

export const TransacoesApi = {
    listar: async (): Promise<Transacao[]> => {
        const response = await http.get<Transacao[]>("/transacoes");
        return response.data;
    },

    criar: async (data: {
        descricao: string;
        valor: number;
        tipo: TipoTransacao;
        pessoaId: string;
        categoriaId: string;
    }): Promise<void> => {
        await http.post("/transacoes", data);
    },
};
