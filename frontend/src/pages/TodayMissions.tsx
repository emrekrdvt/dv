import { useParams } from 'react-router-dom';
import ProjectMissionsTable from '../components/MissionTable';


const TodayMissions: React.FC = () => {
  return (
    <div className='p-8'>
      <ProjectMissionsTable  today={true}/>
    </div>
  );
};

export default TodayMissions;
