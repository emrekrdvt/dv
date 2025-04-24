import React from 'react';
import { useNavigate } from 'react-router-dom';

const NotFoundPage: React.FC = () => {
  const navigate = useNavigate();

  const goanasayfa = () => {
    navigate('/anasayfa');
  };

  return (
    <div style={{ textAlign: 'center', marginTop: '50px' }}>
      <h1>404 - Sayfa Bulunamadı</h1>
      <p>Üzgünüz, aradığınız sayfa mevcut değil.</p>
      <button
        onClick={goanasayfa}
        style={{
          padding: '10px 20px',
          fontSize: '16px',
          backgroundColor: '#007BFF',
          color: '#fff',
          border: 'none',
          borderRadius: '5px',
          cursor: 'pointer',
        }}
      >
        Ana Sayfaya Dön
      </button>
    </div>
  );
};

export default NotFoundPage;