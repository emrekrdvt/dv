export interface MissionUpdate{
    id : number ,
    title: string,
    description: string,
    status: number,
    projectId: number,    
    assignedUserId: number
}

export interface DeleteMissionSuccessResponse {
    success: boolean;
    message: string;
    data: boolean;
}
export interface DeleteMissionFailResponse {
    error : string;
}



export interface MissionCreate {
    title: string,
    description: string,
    status: number,
    projectId: number,
    assignedUserId: number
}


export interface MissionCreateCustomResponse {
    success: boolean;
    message: string;
    data: {
        title: string;
        description: string;
        userId: number;
        projectId: number;
        assignedUserId: number;
    };
    error?: string;
}

export interface MissionUpdateForhistory{
    title: string | undefined,
    description: string | undefined,
    status: number | undefined,
}