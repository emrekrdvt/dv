// src/pages/LoginPage.tsx
import React, { useState } from "react";
import { Input } from "../components/Input";
import toast from "react-hot-toast";
import { useNavigate } from "react-router-dom";
import { login } from "../services/Api";

export const LoginPage = () => {
  const [form, setForm] = useState({
    username: "",
    password: ""
  });

  const navigate = useNavigate();

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      const res = await login(form);
      toast.success(`Hoş geldin ${res.username}`);
      navigate("/projects");
    } catch (error: any) {
    console.log(error.response);
      toast.error(error.response?.data?.error || "Giriş başarısız");
    }
  };

  return (
    <div className="flex items-center justify-center min-h-screen">
      <div className="bg-white p-6 rounded shadow-md w-full max-w-md">
        <h1 className="text-xl font-semibold mb-4 text-center">Giriş Yap</h1>
        <form onSubmit={handleSubmit}>
          <Input name="username" label="Kullanıcı Adı" onChange={handleChange} value={form.username} required />
          <Input name="password" label="Şifre" type="password" onChange={handleChange} value={form.password} required />
          <button type="submit" className="mt-4 w-full bg-indigo-600 text-white py-2 rounded hover:bg-indigo-700">
            Giriş Yap
          </button>
        </form>
      </div>
    </div>
  );
};