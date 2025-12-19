import React, { useEffect, useState } from "react";
import { PessoasApi } from "../api/pessoas";
import { CategoriasApi } from "../api/categorias";
import { TransacoesApi } from "../api/transacoes";
import { Pessoa } from "../types/Pessoa";
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

export function Transacoes() {
    const [descricao, setDescricao] = useState("");
    const [valor, setValor] = useState(0);
    const [tipo, setTipo] = useState(1);
    const [pessoaId, setPessoaId] = useState("");
    const [categoriaId, setCategoriaId] = useState("");
    const [pessoas, setPessoas] = useState<Pessoa[]>([]);
    const [categorias, setCategorias] = useState<Categoria[]>([]);

    useEffect(() => {
        PessoasApi.listar().then(setPessoas);
        CategoriasApi.listar().then(setCategorias);
    }, []);

    const criar = async () => {
        await TransacoesApi.criar({ descricao, valor, tipo, pessoaId, categoriaId });
        setDescricao("");
        setValor(0);
        window.dispatchEvent(new Event("transacao-criada"));
    };

    return (
        <div className="max-w-4xl mx-auto p-6">
            <h2 className="text-2xl font-semibold mb-4">Transações</h2>
            <Card>
                <form onSubmit={(e) => { e.preventDefault(); criar(); }} className="grid grid-cols-1 md:grid-cols-3 gap-3">
                    <Field label="Descrição">
                        <input placeholder="Descrição" value={descricao} onChange={(e) => setDescricao(e.target.value)} className="px-3 py-2 rounded-lg border border-gray-200 shadow-sm focus:outline-none" />
                    </Field>
                    <Field label="Valor">
                        <input type="number" placeholder="Valor" value={valor} onChange={(e) => setValor(Number(e.target.value))} className="px-3 py-2 rounded-lg border border-gray-200 shadow-sm focus:outline-none" />
                    </Field>
                    <Field label="Tipo">
                        <select value={tipo} onChange={(e) => setTipo(Number(e.target.value))} className="px-3 py-2 rounded-lg border border-gray-200 shadow-sm">
                            <option value={1}>Despesa</option>
                            <option value={2}>Receita</option>
                        </select>
                    </Field>
                    <Field label="Pessoa">
                        <select value={pessoaId} onChange={(e) => setPessoaId(e.target.value)} className="px-3 py-2 rounded-lg border border-gray-200 shadow-sm">
                            <option value="">Pessoa</option>
                            {pessoas.map(p => <option key={p.id} value={p.id}>{p.nome}</option>)}
                        </select>
                    </Field>
                    <Field label="Categoria">
                        <select value={categoriaId} onChange={(e) => setCategoriaId(e.target.value)} className="px-3 py-2 rounded-lg border border-gray-200 shadow-sm">
                            <option value="">Categoria</option>
                            {categorias.map(c => <option key={c.id} value={c.id}>{c.descricao}</option>)}
                        </select>
                    </Field>
                    <div className="flex items-end gap-2 md:justify-end">
                        <button type="submit" className="px-4 py-2 rounded-lg bg-indigo-600 text-white font-medium">Criar</button>
                        <button type="button" onClick={() => { PessoasApi.listar().then(setPessoas); CategoriasApi.listar().then(setCategorias); }} className="px-3 py-2 rounded-lg border">Atualizar</button>
                    </div>
                </form>
            </Card>
        </div>
    );
}
