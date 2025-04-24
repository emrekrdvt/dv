export interface ProjectResponse {
  projectId: number;
  name: string;
  description: string;
  customer: {
    id: number;
    name: string;
  };
  missions: Mission[];
  projectUsers: ProjectUser[];
}

export interface Mission {
  id: number;
  title: string;
  description: string;
  createdAt: string;
  status: {
    status: number;
    statusName: string;
  };
}

export interface ProjectUser {
  userId: number;
  username: string;
}

export interface MissionWithAssignedUser extends Mission {
  assignedUser: {
    userId: number;
    username: string;
  };
}