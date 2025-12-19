import { http } from "./http";
import { Pessoa } from "../types/Pessoa";

export const PessoasApi = {
    listar: async (): Promise<Pessoa[]> => {
        const response = await http.get<Pessoa[]>("/pessoas");
        return response.data;
    },

    criar: async (nome: string, idade: number): Promise<void> => {
        await http.post("/pessoas", { nome, idade });
    },

    deletar: async (id: string): Promise<void> => {
        await http.delete(`/pessoas/${id}`);
    },
};
