import { BabyModel, getBaby } from "../network/BabyModule";
import { QueryClient, QueryClientProvider, useQuery } from 'react-query';
import { isDevelopment } from "../utilities/EnvManager";
import moment from 'moment'


const queryClient = new QueryClient()

const showAgeByWeeks = (dateTime: string) => {
    const birthDate = new Date(parseInt(dateTime));
    const curDate = new Date();
    const elapsed = curDate.getTime() - birthDate.getTime();
    const ageByWeeks = moment.duration(elapsed).asWeeks();
    const weeks = Math.floor(ageByWeeks);
    console.log(Math.abs(weeks - ageByWeeks));
    console.log(7 * Math.abs(weeks - ageByWeeks));
    const remainingDays = Math.floor(7 * Math.abs(weeks - ageByWeeks));
    const ageCount = `${weeks} weeks and ${remainingDays} days`
    return ageCount;
};

const showDueDate = (dateTime: string) => {
    const dueDate = new Date(parseInt(dateTime));
    return dueDate.toLocaleDateString();
};

const getBabyRoomImgUrl = () => {
    const imagePath = "baby_room.jpg";
    const assetPath = isDevelopment() ? `/public/${imagePath}` : `/${imagePath}`;
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
                                <span className="font-bold">{showAgeByWeeks(baby.age)}</span>
                                <span> old</span>
                            </div>
                            <p className="text-gray-700 text-base">
                                Will be meeting you on {showDueDate(baby.dueDate)}
                            </p>
                        </div>
                    </div>
                </div>
            </>
        )
    }
}
