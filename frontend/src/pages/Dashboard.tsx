import React, { useEffect, useState } from "react";
import { DashboardApi, DashboardResponse } from "../api/dashboard";

function Card({ children, className = "" }: { children: React.ReactNode; className?: string }) {
    return <div className={`bg-white/80 backdrop-blur-sm shadow-md rounded-2xl p-4 ${className}`}>{children}</div>;
}

export function Dashboard() {
    const [dados, setDados] = useState<DashboardResponse | null>(null);

    const carregar = async () => {
        const data = await DashboardApi.obterTotais();
        setDados(data);
    };

    useEffect(() => {
        carregar();
        const handler = () => carregar();
        window.addEventListener("transacao-criada", handler);
        return () => { window.removeEventListener("transacao-criada", handler); };
    }, []);

    const fmt = (n: number) => new Intl.NumberFormat("pt-BR", { style: "currency", currency: "BRL" }).format(n);

    return (
        <div className="max-w-6xl mx-auto p-6">
            <h2 className="text-2xl font-semibold mb-4">Dashboard</h2>

            <div className="grid grid-cols-1 md:grid-cols-3 gap-4 mb-6">
                <Card>
                    <div className="text-sm text-gray-500">Total Receitas</div>
                    <div className="text-xl font-semibold">{dados ? fmt(dados.totaisGerais.totalReceitas) : "—"}</div>
                </Card>
                <Card>
                    <div className="text-sm text-gray-500">Total Despesas</div>
                    <div className="text-xl font-semibold">{dados ? fmt(dados.totaisGerais.totalDespesas) : "—"}</div>
                </Card>
                <Card>
                    <div className="text-sm text-gray-500">Saldo</div>
                    <div className="text-xl font-semibold">{dados ? fmt(dados.totaisGerais.saldo) : "—"}</div>
                </Card>
            </div>

            <div className="overflow-x-auto bg-white/60 rounded-2xl p-3 shadow-sm">
                <table className="w-full table-fixed text-sm">
                    <thead>
                        <tr className="text-left text-gray-600">
                            <th className="px-3 py-2 w-2/6">Pessoa</th>
                            <th className="px-3 py-2 w-1/6">Receitas</th>
                            <th className="px-3 py-2 w-1/6">Despesas</th>
                            <th className="px-3 py-2 w-1/6">Saldo</th>
                        </tr>
                    </thead>
                    <tbody>
                        {dados?.pessoas.map(p => (
                            <tr key={p.pessoaId} className="odd:bg-white even:bg-gray-50">
                                <td className="px-3 py-2 align-top">{p.nome}</td>
                                <td className="px-3 py-2 align-top">{fmt(p.totalReceitas)}</td>
                                <td className="px-3 py-2 align-top">{fmt(p.totalDespesas)}</td>
                                <td className="px-3 py-2 align-top">{fmt(p.saldo)}</td>
                            </tr>
                        ))}
                    </tbody>
                </table>
                {(!dados || dados.pessoas.length === 0) && <div className="text-gray-500 text-center py-6">Nenhuma pessoa com dados para exibir.</div>}
            </div>
        </div>
    );
}