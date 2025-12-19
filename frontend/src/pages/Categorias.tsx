import React, { useEffect, useState } from "react";
import { CategoriasApi } from "../api/categorias";
import { Categoria } from "../types/Categoria";

function Card({ children, className = "" }: { children: React.ReactNode; className?: string }) {
    return <div className={`bg-white/80 backdrop-blur-sm shadow-md rounded-2xl p-4 ${className}`}>{children}</div>;
}
function Field({ label, children }: { label: string; children: React.ReactNode }) {
    return (
        <label className="flex flex-col gap-2 text-sm">
            <span className="text-gray-600 font-medium">{label}</span>
            {children}
        </label>
    );
}

export function Categorias() {
    const [categorias, setCategorias] = useState<Categoria[]>([]);
    const [descricao, setDescricao] = useState("");
    const [finalidade, setFinalidade] = useState(1);

    const carregar = async () => {
        const data = await CategoriasApi.listar();
        setCategorias(data);
    };

    useEffect(() => { carregar(); }, []);

    const criar = async () => {
        await CategoriasApi.criar({ descricao, finalidade });
        setDescricao("");
        carregar();
    };
    const deletar = async (id: string) => { await CategoriasApi.deletar(id); carregar(); };

    const finalidadeLabel = (v: number) => (v === 1 ? "Despesa" : v === 2 ? "Receita" : "Ambas");

    return (
        <div className="max-w-4xl mx-auto p-6">
            <h2 className="text-2xl font-semibold mb-4">Categorias</h2>
            <Card className="mb-6">
                <form onSubmit={(e) => { e.preventDefault(); criar(); }} className="grid grid-cols-1 md:grid-cols-3 gap-3 items-end">
                    <Field label="Descrição">
                        <input placeholder="Descrição" value={descricao} onChange={(e) => setDescricao(e.target.value)} className="px-3 py-2 rounded-lg border border-gray-200 shadow-sm focus:outline-none" />
                    </Field>
                    <Field label="Finalidade">
                        <select value={finalidade} onChange={(e) => setFinalidade(Number(e.target.value))} className="px-3 py-2 rounded-lg border border-gray-200 shadow-sm">
                            <option value={1}>Despesa</option>
                            <option value={2}>Receita</option>
                            <option value={3}>Ambas</option>
                        </select>
                    </Field>
                    <div className="flex gap-2 md:justify-end">
                        <button type="submit" className="px-4 py-2 rounded-lg bg-indigo-600 text-white font-medium">Criar</button>
                        <button type="button" onClick={carregar} className="px-3 py-2 rounded-lg border">Atualizar</button>
                    </div>
                </form>
            </Card>
            <div className="grid gap-3">
                {categorias.length === 0 ? <Card>Sem categorias cadastradas.</Card> : categorias.map(c => (
                    <div key={c.id} className="flex items-center justify-between gap-4 bg-white/60 p-3 rounded-xl border border-gray-100 shadow-sm">
                        <div>
                            <div className="font-medium">{c.descricao}</div>
                            <div className="text-xs text-gray-500">Finalidade: {finalidadeLabel(c.finalidade)}</div>
                        </div>
                        <button onClick={() => deletar(c.id)} className="px-3 py-1 rounded-lg bg-red-500 text-white text-sm hover:bg-red-600">Deletar</button>
                    </div>
                ))}
            </div>
        </div>
    );
}
