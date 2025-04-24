import { useParams } from 'react-router-dom';
import ProjectMissionsTable from '../components/MissionTable';


const ProjectDetail: React.FC = () => {
  const { projectId } = useParams();

  if (!projectId) return <div>Geçersiz proje ID</div>;

  return (
    <div className='p-8'>
      <ProjectMissionsTable projectId={Number(projectId)} />
    </div>
  );
};

export default ProjectDetail;
