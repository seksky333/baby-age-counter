import { isDevelopment } from "../utilities/EnvManager";

const SERVER = "http://localhost:9090"


export interface BabyModel {
    id: string,
    age: string,
    dueDate: string
}

export async function getBaby() {
    const url = isDevelopment() ? `${SERVER}/baby` : "/baby";
    const response = await fetch(url);
    return await response.json();
}