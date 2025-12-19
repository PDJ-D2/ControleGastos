import React, { useState } from "react";
import { Pessoas } from "./pages/Pessoas";
import { Categorias } from "./pages/Categorias";
import { Transacoes } from "./pages/Transacoes";
import { Dashboard } from "./pages/Dashboard";

export default function App() {
  const tabs = ["Dashboard", "Pessoas", "Categorias", "Transações"];
  const [active, setActive] = useState(0);

  return (
    <div className="min-h-screen bg-gradient-to-b from-slate-50 to-slate-100 p-6">
      <div className="max-w-6xl mx-auto">
        <h1 className="text-3xl font-bold mb-6">Home Expenses Control</h1>

        <nav className="flex gap-2 mb-6">
          {tabs.map((t, i) => (
            <button
              key={t}
              onClick={() => setActive(i)}
              className={`px-4 py-2 rounded-lg font-medium ${active === i ? 'bg-indigo-600 text-white' : 'bg-white border'}`}
            >
              {t}
            </button>
          ))}
        </nav>

        <div className="bg-transparent">
          {active === 0 && <Dashboard />}
          {active === 1 && <Pessoas />}
          {active === 2 && <Categorias />}
          {active === 3 && <Transacoes />}
        </div>
      </div>
    </div>
  );
}
