import { http } from "./http";

export interface TotaisPessoa {
    pessoaId: string;
    nome: string;
    totalReceitas: number;
    totalDespesas: number;
    saldo: number;
}

export interface DashboardResponse {
    pessoas: TotaisPessoa[];
    totaisGerais: {
        totalReceitas: number;
        totalDespesas: number;
        saldo: number;
    };
}

export const DashboardApi = {
    obterTotais: async (): Promise<DashboardResponse> => {
        const res = await http.get("/dashboard/totais");
        return res.data;
    },
};