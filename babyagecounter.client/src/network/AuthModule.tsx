import { isDevelopment, serverAddress } from "../utilities/EnvManager";


export interface IdentityModel {
    token: string,
    refreshToken: string
}

export interface LoginModel {
    email: string,
    password: string
}

export async function login(loginModel: LoginModel) {
    const authServerAddress = "http://localhost:5194";
    console.log(loginModel);

    const url = isDevelopment() ? `${authServerAddress}/login` : "/login";
    console.log(url);
    
    const response = await fetch(url,{
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(loginModel)
    });
    return await response.json();
}