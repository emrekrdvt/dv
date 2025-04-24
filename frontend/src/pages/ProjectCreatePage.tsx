import React, { useState } from "react";
import { Input } from "../components/Input";
import { createProject } from "../services/Api";
import toast from "react-hot-toast";
import { MultiSelectDropdown } from "../components/MultiSelect";

export const ProjectCreatePage = () => {
  const [form, setForm] = useState({
    name: "",
    description: "",
    customerId: 0,
    projectUserIds: [] as number[]
  });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      const payload = {
        name: form.name,
        description: form.description,
        customerId: Number(form.customerId),
        projectUserIds: form.projectUserIds
      };
      const res = await createProject(payload);
      toast.success("Proje oluşturuldu: ID " + res.data.data.projectId);
    } catch (error: any) {
      toast.error(error.response?.data?.error || "Hata oluştu");
    }
  };

  return (
    <div className="max-w-xl mx-auto p-6">
      <h1 className="text-xl font-semibold mb-4">Yeni Proje Oluştur</h1>
      <form onSubmit={handleSubmit}>
        <Input name="name" label="Proje Adı" onChange={handleChange} value={form.name} required />
        <Input name="description" label="Açıklama" onChange={handleChange} value={form.description} />
        <Input flag1 name="customerId" label="Müşteri" value={form.customerId} onChange={handleChange} required />

        <MultiSelectDropdown
          selected={form.projectUserIds}
          onChange={(newSelected: number[]) => setForm({ ...form, projectUserIds: newSelected })}
        />

        <button
          type="submit"
          className="mt-4 w-full bg-indigo-600 text-white py-2 rounded hover:bg-indigo-700"
        >
          Oluştur
        </button>
      </form>
    </div>
  );
};
