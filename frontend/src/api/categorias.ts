import { http } from "./http";
import { Categoria, FinalidadeCategoria } from "../types/Categoria";

export const CategoriasApi = {
    listar: async (): Promise<Categoria[]> => {
        const response = await http.get<Categoria[]>("/categorias");
        return response.data;
    },

    criar: async (categoria: { descricao: string; finalidade: number }) => {
        await fetch("http://localhost:5110/categorias", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(categoria),
        });
    },
    deletar: async (id: string) => {
        await fetch(`http://localhost:5110/categorias/${id}`, {
            method: "DELETE",
        });
    },
};
