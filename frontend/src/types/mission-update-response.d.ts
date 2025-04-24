
export interface MissionCustomResponse {
    success: boolean;
    message: string;
    data: MissionDto[]
  }
  
  export interface MissionDto {
    id: number;
    title: string;
    description: string;
    status: number;
    projectId: number;
    assignedUserId: number;
  }
