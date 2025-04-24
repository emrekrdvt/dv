export interface MissionGetResponse {
    success: boolean;
    message: string;
    data: Mission[];
}

export interface Mission {
    id: number;
    title: string;
    description: string;
    createdAt: string;
    status: MissionStatus;
    assignedUser: AssignedUser;
}

export interface MissionStatus {
    status: number;
    statusName: string;
}

export interface AssignedUser {
    userId: number;
    username: string;
}
