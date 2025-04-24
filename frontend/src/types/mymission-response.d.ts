export interface MyMissionResponse {
    success: boolean;
    message: string;
    data: MissionData[];
}

export interface MissionData {
    id: number;
    name: string;
    description: string;
    myMissionProject: MyMissionProject;
    status: Stts;

}

export interface MyMissionProject {
    name: string;
    description: string;
    customerId: number;
    customer: Customer;
    id: number;
    createdat: string;
}

export interface Customer {
    name: string;
}

export interface Stts{
    status: number;
    statusName: string;
}