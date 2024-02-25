import { isDevelopment, serverAddress } from "../utilities/EnvManager";


export interface IdentityModel {
    token: string,
    refreshToken: string
}

export interface LoginModel {
    email: string,
    password: string
}

export async function login() {
    const url = isDevelopment() ? `${serverAddress}/login` : "/login";
    const response = await fetch(url);
    return await response.json();
}