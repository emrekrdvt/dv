import React from "react";
import { BrowserRouter as Router, Route, Routes, Navigate } from "react-router-dom";
import { Toaster } from "react-hot-toast";
import { ProjectCreatePage } from "./pages/ProjectCreatePage";
import { LoginPage } from "./pages/LoginPage";
import { Layout } from "./components/Layout";
import { decodeToken } from "./services/TokenDecode";
import NotFoundPage from "./pages/404";
import AnaSayfa from "./pages/AnaSayfa";
import AllProject from "./pages/AllProject";
import ProjectDetail from "./pages/TaskDetail";
import AdminPage from "./pages/Admin";
import MyMissions from "./pages/MyMissions";
import TodayMissions from "./pages/TodayMissions";


const Token: React.FC<{ children: React.ReactNode }> = ({ children }) => {

  const token = decodeToken();

  return token ? <>{children}</> : <Navigate to="/login" />;
};

const CanILogin: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const token = decodeToken();
  const checkAdmin = token?.isAdmin === "True";

  if (checkAdmin === undefined) {
    return <Navigate to="/404" />; // Token yoksa 404'e yönlendir
  }

  return checkAdmin ? <>{children}</> : <Navigate to="/mytasks" />; // Admin değilse 404'e yönlendir
};


export default function App() {
  return (
    <Router>
      <Toaster position="top-right" />
      <div className="min-h-screen bg-gray-50 text-gray-900">
        <Routes>
          <Route path="/" element={<Navigate to="/login" />} />
          <Route path="/login" element={<LoginPage />} />
          <Route
            path="/projects/create"
            element={
              <CanILogin>
                <Token>
                  <Layout title="Proje Oluştur">
                    <ProjectCreatePage />
                  </Layout>
                </Token>
              </CanILogin>

            }
          />
          <Route path="/404" element={<NotFoundPage />} />
          <Route path="*" element={<NotFoundPage />} />
          <Route
            path="/anasayfa"
            element={
              <Token>
                <Layout title="Ana Sayfa">
                  <AnaSayfa />
                </Layout>
              </Token>}
          />
          <Route path="/projects" element={
            <Layout title="Tüm Görevler">
              <AllProject />
            </Layout>
          } />

          <Route path="/project/:projectId" element={
            <Token>
              <Layout title="Proje Görevleri">
                <ProjectDetail />
              </Layout>
            </Token>
          } />
          <Route path="/admin" element={
            <Token>
              <Layout title="ADMIN">
                <AdminPage />
              </Layout>
            </Token>
          } />

          <Route path="/myprojects" element={
            <Token>
              <Layout title="Tüm Görevlerim">
                <MyMissions/>
              </Layout>
            </Token>
          } />
          <Route path="/todaymissions" element={
            <Token>
              <Layout title="Bugünün Görevleri">
                <TodayMissions/>
              </Layout>
            </Token>
          } />


        </Routes>
    
      </div>
    </Router>
  );
}