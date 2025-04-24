
import React from "react";
import { Link, useNavigate } from "react-router-dom";
import { decodeToken } from "../services/TokenDecode";
import toast from "react-hot-toast";

export const Layout: React.FC<LayoutProps> = ({ children, title = "TaskManagment" }) => {
  const navigate = useNavigate();
  const token = decodeToken();
  console.log(token);
  const isAdmin = token?.isAdmin === "True";

  const handleLogout = () => {
    localStorage.clear();
    navigate("/login");
  };

  const handleProjectCreateClick = (e: React.MouseEvent) => {
    if (!isAdmin) {
      e.preventDefault();
      toast.error("Bu sayfaya erişim izniniz yok. Ana sayfaya yönlendiriliyorsunuz.");
      navigate("/anasayfa");
    }
  }

  return (
    <div className="min-h-screen   text-gray-900 flex">

      <aside className="w-60 bg-white shadow-md hidden sm:block">
        <div className="p-[18px] font-bold border-b border-gray-200">Menü</div>
        <nav className="p-4">
          <ul className="space-y-2 text-sm">
            <li><Link to="/projects/create" onClick={handleProjectCreateClick} className="block px-2 py-1 rounded hover:bg-indigo-100">Proje Oluştur</Link></li>
            <li><Link to="/projects" className="block px-2 py-1 rounded hover:bg-indigo-100">Projeler</Link></li>
            
            <li><Link to="/myprojects" className="block px-2 py-1 rounded hover:bg-indigo-100">Tüm Görevlerim</Link></li>

            <li><Link to="/todaymissions" className="block px-2 py-1 rounded hover:bg-indigo-100">Bugünün Görevleri</Link></li>
            {isAdmin && <li><Link to="/admin" className="block px-2 py-1 rounded hover:bg-indigo-100">Admin</Link></li>}
            <li><button onClick={handleLogout} className="w-full text-left px-2 py-1 rounded hover:bg-indigo-100">Çıkış Yap</button></li>
          </ul>
        </nav>
      </aside>


      <div className="flex-1">

        <header className="bg-white shadow px-6 py-4 flex items-center justify-between" >
          <h1 className="text-lg font-semibold">{title}</h1>
        </header>

        <main className="p-6">
          {children}
        </main>

      </div>
    </div>
  );
};