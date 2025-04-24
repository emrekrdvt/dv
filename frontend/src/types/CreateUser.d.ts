export interface CreateUserModel {
    id?: number;
    email: string;
    username: string;
    admin: boolean;
    isDelMethod?: boolean;
}