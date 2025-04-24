import { useParams } from 'react-router-dom';
import ProjectMissionsTable from '../components/MissionTable';


const MyMissions: React.FC = () => {
  return (
    <div className='p-8'>
      <ProjectMissionsTable  />
    </div>
  );
};

export default MyMissions;
