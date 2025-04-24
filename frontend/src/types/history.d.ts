export interface CreateHistoryRequest {
    userId: number;
    missionId: number;
    action: string;
    projectId: number;
}

export interface HistoryResponse {
    success: boolean;
    message: string;
    data: HistoryReponseData[];
}

export interface HistoryReponseData {
    action: string;
    username: string;
    missionTitle: string;
    timestamp: string;
}
