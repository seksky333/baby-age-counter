import { BabyModel, getBaby } from "../network/BabyModule";
import { QueryClient, QueryClientProvider, useQuery } from 'react-query';

const queryClient = new QueryClient()


export default function AgeCounter() {
    return (
        <QueryClientProvider client={queryClient}>
            <BabyAgeContent />
        </QueryClientProvider>
    )
}

function BabyAgeContent() {
    const { isLoading, data } = useQuery<BabyModel[]>({
        queryKey: ['baby'],
        queryFn: getBaby
    });

    if (isLoading) return <div>Fetching posts...</div>;
    // if (error) return <div>An error occurred: {(error as Error).message}</div>;
    const babyList = data ?? [];
    const baby: BabyModel | null = babyList.length > 0 ? babyList[0] : null;

    if (baby == null) {
        return (
            <>
                <h1>No baby found</h1>
            </>
        )
    } else {
        return (
            <>
                <h1>Hello Baby </h1>
                <p>You are {baby.age}</p>
                <p>Due at {baby.age}</p>
            </>
        )
    }
}