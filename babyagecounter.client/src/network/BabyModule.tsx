import { isDevelopment, serverAddress } from "../utilities/EnvManager";


export interface BabyModel {
    id: string,
    age: string,
    dueDate: string
}

export async function getBaby() {
    const authorizationToken = "TOKEN_TO_BE_IMPLEMENTED";
    const url = isDevelopment() ? `${serverAddress}/baby` : "/baby";
    const response = await fetch(url,{
        headers:{
            "Content-Type": "application/json",
            "Authorization": `Bearer ${authorizationToken}`
        }
    });
    return await response.json();
}