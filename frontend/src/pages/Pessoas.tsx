import React, { useEffect, useState } from "react";
import { PessoasApi } from "../api/pessoas";
import { Pessoa } from "../types/Pessoa";

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

export function Pessoas() {
    const [pessoas, setPessoas] = useState<Pessoa[]>([]);
    const [nome, setNome] = useState("");
    const [idade, setIdade] = useState<number>(0);

    const carregar = async () => { const data = await PessoasApi.listar(); setPessoas(data); };

    useEffect(() => { carregar(); }, []);

    const criar = async () => { await PessoasApi.criar(nome, idade); setNome(""); setIdade(0); carregar(); };
    const deletar = async (id: string) => { await PessoasApi.deletar(id); carregar(); };

    return (
        <div className="max-w-4xl mx-auto p-6">
            <h2 className="text-2xl font-semibold mb-4">Pessoas</h2>

            <Card className="mb-6">
                <form onSubmit={(e) => { e.preventDefault(); criar(); }} className="grid grid-cols-1 md:grid-cols-3 gap-3 items-end">
                    <Field label="Nome">
                        <input placeholder="Nome" value={nome} onChange={(e) => setNome(e.target.value)} className="px-3 py-2 rounded-lg border border-gray-200 shadow-sm focus:outline-none" />
                    </Field>
                    <Field label="Idade">
                        <input type="number" placeholder="Idade" value={idade} onChange={(e) => setIdade(Number(e.target.value))} className="px-3 py-2 rounded-lg border border-gray-200 shadow-sm focus:outline-none" />
                    </Field>
                    <div className="flex gap-2 md:justify-end">
                        <button type="submit" className="px-4 py-2 rounded-lg bg-indigo-600 text-white font-medium">Criar</button>
                        <button type="button" onClick={carregar} className="px-3 py-2 rounded-lg border">Atualizar</button>
                    </div>
                </form>
            </Card>

            <ul className="space-y-3">
                {pessoas.length === 0 && <Card>Nenhuma pessoa cadastrada.</Card>}
                {pessoas.map(p => (
                    <li key={p.id} className="flex items-center justify-between gap-4 bg-white/60 p-3 rounded-xl border border-gray-100 shadow-sm">
                        <div>
                            <div className="font-medium">{p.nome}</div>
                            <div className="text-xs text-gray-500">{p.idade} anos</div>
                        </div>
                        <button onClick={() => deletar(p.id)} className="px-3 py-1 rounded-lg bg-red-500 text-white hover:bg-red-600">Deletar</button>
                    </li>
                ))}
            </ul>
        </div>
    );
}
