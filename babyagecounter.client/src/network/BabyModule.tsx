const server = "http://localhost:5254"


export interface BabyModel {
    id: string,
    age: string,
    dueDate: string
}

export async function getBaby() {
    const url = `${server}/baby`;
    const response = await fetch(url);
    return  await response.json();
}