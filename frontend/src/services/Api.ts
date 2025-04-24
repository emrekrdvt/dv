import axios from "axios";
import { LoginRequest, LoginResponse } from "../types/auth";
import { Mission, ProjectResponse } from "../types/project";
import { UserResponse } from "../types/userresponse";
import { CustomerResponse } from "../types/customer-response";
import { MissionCustomResponse, MissionDto } from "../types/mission-update-response";
import {
  DeleteMissionFailResponse,
  DeleteMissionSuccessResponse,
  MissionCreate,
  MissionCreateCustomResponse,
  MissionUpdate,
  MissionUpdateForhistory,
} from "../types/mission-update";
import { CreateUserModel } from "../types/CreateUser";
import { CreateHistoryRequest, HistoryResponse } from "../types/history";
import { MissionGetResponse } from "../types/mission-get-response";
import { MyMissionResponse } from "../types/mymission-response";
 
const api = axios.create({
  baseURL: process.env.REACT_APP_API_ENDPOINT
});
 


api.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem("token");
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    console.log(config.headers);
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

export const createProject = (data: any) => {
  console.log(data);
  return api.post("/project", data);
};

export const updateMission = async (
  data: MissionUpdate,
  oldData: MissionUpdateForhistory | undefined
): Promise<MissionCustomResponse> => {
  try {
    const statusMap: Record<number, string> = {
      0: "Bekliyor",
      1: "Devam Ediyor",
      2: "Tamamlandı",
    };

    const response = await api.put<MissionCustomResponse>(`/mission`, data);
    const updatedData = response.data.data;

    const historyData: CreateHistoryRequest = {
      action: `${data.title} - ${data.description} - ${
        statusMap[data.status]
      }  --> ${oldData?.title} - ${oldData?.description} - ${
        oldData && oldData.status !== undefined ? statusMap[oldData.status] : "N/A"
      }`,
      userId: data.assignedUserId,
      missionId: data.id,
      projectId: data.projectId,
    };

    await createHistory(historyData);

    console.log("Mission updated successfully:", response.data);
    return response.data;
  } catch (error) {
    console.error("Error updating mission:", error);
    throw error;
  }
};

export const login = async (data: LoginRequest): Promise<LoginResponse> => {
  const res = await api.post<LoginResponse>("/auth/login", data);
  localStorage.setItem("token", res.data.token);
  localStorage.setItem("username", res.data.username);
  return res.data;
};

export const getProject = async (
  projectId?: number
): Promise<ProjectResponse[]> => {
  
  const endpoint = projectId ? `/project/${projectId}` : "/project";
  const response = await api.get<ProjectResponse[]>(endpoint);
  return response.data;
};

export const getProjectMissions = async (projectId?: number) : Promise<MissionGetResponse> => {
  const endpoint = `/mission/${projectId}`;
  const response = await api.get<MissionGetResponse>(endpoint);
  return response.data;
}

export const getMyMissions = async (today?:boolean): Promise<MyMissionResponse> => {
  //const endpoint = `/mission/myMissions`;
  const endpoint = today ? `/mission/myMissions?today=true` : `/mission/myMissions`;
  const response = await api.get<MyMissionResponse>(endpoint);
  return response.data;
}



export const getUsers = async (userid?: number): Promise<UserResponse> => {
  const endpoint = userid ? `/user/${userid}` : "/user/-1";
  const response = await api.get<UserResponse>(endpoint);
  return response.data;
};
export const getCustomer = async (
  customerid?: number
): Promise<CustomerResponse> => {
  const endpoint = customerid ? `/customer/${customerid}` : "/customer/-1";
  const response = await api.get<CustomerResponse>(endpoint);
  return response.data;
};

export const deleteMission = async (
  id: number
): Promise<DeleteMissionSuccessResponse | DeleteMissionFailResponse> => {
  const response = await api.delete<
    DeleteMissionSuccessResponse | DeleteMissionFailResponse
  >(`/mission/${id}`);
  if (response.status === 200) {
    return response.data;
  } else {
    return response.data;
  }
};

export const createMission = async (
  data: MissionCreate
): Promise<MissionCreateCustomResponse> => {
  const response = await api.post<MissionCreateCustomResponse>(
    "/mission",
    data
  );

  return response.data;
};

export const createUser = async (data: CreateUserModel, update: boolean) => {
  if (!update) return api.post("/admin/createUser", data);
  if (data.isDelMethod) {
    console.log(data);
    return api.put("/admin/updateUser", data);
  } else if (update) return api.put("/admin/updateUser", data);
};

export const createHistory = async (
  data: CreateHistoryRequest
): Promise<HistoryResponse> => {
  const response = await api.post<HistoryResponse>(
    "/admin/createHistory",
    data
  );
  console.log(response);
  return response.data;
};

export const getHistory = async (
  projectid?: number
): Promise<HistoryResponse> => {
  const endpoint = `/admin/getProjectHistory/?ProjectId=${projectid}`;
  const response = await api.get<HistoryResponse>(endpoint);
  return response.data;
};

export const createCustomer = async (
 data : any
): Promise<CustomerResponse> => {
  const response = await api.post<CustomerResponse>("/customer", data);
  return response.data;
};
