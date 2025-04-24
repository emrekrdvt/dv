import React from 'react';
import { useNavigate } from 'react-router-dom';
import MyTable from '../components/Project';

const AllProject: React.FC = () => {
  
  return (
    <div style={{ textAlign: 'center', marginTop: '50px' }}>
      <MyTable></MyTable>
    </div>
  );
};

export default AllProject;