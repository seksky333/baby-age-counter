import {BabyModel, getBaby} from "../network/BabyModule";
import {QueryClient, QueryClientProvider, useQuery} from 'react-query';
import {isDevelopment} from "../utilities/EnvManager";
import moment from 'moment-timezone'

const queryClient = new QueryClient()

const showCurrentAgeByWeek = (dateTime: string) => {
    const birthDate = moment(dateTime).valueOf();
    const curDate = moment(new Date().getTime()).valueOf();
    
    const elapsedInMillis = curDate - birthDate;
    const elapsedByWeeks = moment.duration(elapsedInMillis).asWeeks();
    const weeks = Math.floor(elapsedByWeeks);
    const remainingDays = Math.round(7 * Math.abs(weeks - elapsedByWeeks));
    return `${weeks} weeks and ${remainingDays} days`;
};

const showBirthDateAndTime = (dateTime: string) => {
    // const dueDate = new Date(parseInt(dateTime));
    const birthdate = moment(dateTime);
    birthdate.utcOffset()
    return birthdate.format("LLLL");
};

const showDueDate = (dateTime: string) => {
    const dueDate = new Date(parseInt(dateTime));
    return dueDate.toLocaleDateString();
};

const getBabyImgUrl = () => {
    const imagePath = "baby.jpg";
    const assetPath = isDevelopment() ? `/public/${imagePath}` : `/${imagePath}`;
    return new URL(`${assetPath}`, import.meta.url).href;
};


export default function AgeCounter() {
    return (
        <QueryClientProvider client={queryClient}>  
            <BabyAgeContent/>
        </QueryClientProvider>
    )
}

function BabyAgeContent() {
    const {isLoading, data} = useQuery<BabyModel[]>({
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
                        <img className="w-full" src={getBabyImgUrl()} alt="Sunset in the mountains"></img>
                        <div className="px-6 py-4">
                            <div className="text-xl mb-2">
                                <span>I'm </span>
                                <span className="font-bold">{showCurrentAgeByWeek(baby.age)}</span>
                                <span> old</span>
                            </div>
                            <section className="py-4">
                                <p className="text-gray-700 text-base">
                                    My birth date and time is {showBirthDateAndTime(baby.age)}
                                </p>
                                <p className="text-gray-700 text-base">
                                    My due date was: {showDueDate(baby.dueDate)}
                                </p>
                            </section>
                        </div>
                    </div>
                </div>
            </>
        )
    }
}
