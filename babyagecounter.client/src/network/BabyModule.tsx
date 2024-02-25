import { isDevelopment, serverAddress } from "../utilities/EnvManager";


export interface BabyModel {
    id: string,
    age: string,
    dueDate: string
}

export async function getBaby() {
    const url = isDevelopment() ? `${serverAddress}/baby` : "/baby";
    const response = await fetch(url);
    return await response.json();
}