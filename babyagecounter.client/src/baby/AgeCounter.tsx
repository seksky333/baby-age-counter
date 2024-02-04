import { BabyModel, getBaby } from "../network/BabyModule";
import { QueryClient, QueryClientProvider, useQuery } from 'react-query';
import { isDevelopment } from "../utilities/EnvManager";


const queryClient = new QueryClient()

const toWeeks = (dateTime: string) => {
    return dateTime;
};

const getBabyRoomImgUrl = () => {
    const imagePath = "baby_room.jpg";
    const assetPath = isDevelopment() ? `/public/${imagePath}` : `/${imagePath}`;
    console.log(assetPath);
    return new URL(`${assetPath}`, import.meta.url).href;
};


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
                <div className="flex flex-col items-center">
                    <div className="max-w-sm rounded overflow-hidden shadow-lg">
                        <img className="w-full" src={getBabyRoomImgUrl()} alt="Sunset in the mountains"></img>
                        <div className="px-6 py-4">
                            <div className="text-xl mb-2">
                                <span>I'm </span>
                                <span className="font-bold">{toWeeks(baby.age)}</span>
                                <span> old</span>
                            </div>
                            <p className="text-gray-700 text-base">
                                Will be meeting you on {baby.age}
                            </p>
                        </div>
                    </div>
                </div>
            </>
        )
    }
}
