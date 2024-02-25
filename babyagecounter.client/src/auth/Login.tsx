import { QueryClient, QueryClientProvider, useQuery } from 'react-query';
import { IdentityModel, login } from '../network/AuthModule';

const queryClient = new QueryClient()

export default function Login() {
    return (
        <QueryClientProvider client={queryClient}>
            <LoginContent />
        </QueryClientProvider>
    )
}



function LoginContent() {
    const { isLoading, isError, data, refetch } = useQuery<IdentityModel>({
        queryKey: ['login'],
        queryFn: login,
        enabled: false
    });

    const handleLogin = ()=>{
        refetch();
    };
    
    if (isLoading) return <div>Logging in...</div>;

    // const identity: IdentityModel | null = data != null ? data : null
    if (isError) {
        return (
            <>
                <h1>Failed to login</h1>
            </>
        )
    } else {
        if (isError) {
            return (
                <>
                    <h1>Login succeeded! To something!</h1>
                </>
            )
        }

        return (
            <>
                <div className="flex min-h-full flex-1 flex-col justify-center px-6 lg:px-8">
                    <div className="sm:mx-auto sm:w-full sm:max-w-sm">
                        <h2 className="mt-2 text-center text-2xl font-bold leading-9 tracking-tight text-gray-900">
                            Sign in to your account
                        </h2>
                    </div>

                    <div className="mt-10 sm:mx-auto sm:w-full sm:max-w-sm">
                        <form className="space-y-6" action="#" method="POST">
                            <div>
                                <label htmlFor="email" className="block text-sm font-medium leading-6 text-gray-900">
                                    Email address
                                </label>
                                <div className="mt-2">
                                    <input
                                        id="email"
                                        name="email"
                                        type="email"
                                        autoComplete="email"
                                        required
                                        className="block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-green-400 sm:text-sm sm:leading-6"
                                    />
                                </div>
                            </div>

                            <div>
                                <div className="flex items-center justify-between">
                                    <label htmlFor="password" className="block text-sm font-medium leading-6 text-gray-900">
                                        Password
                                    </label>
                                    <div className="text-sm">
                                        <a href="#" className="font-semibold text-green-400 hover:text-green-300">
                                            Forgot password?
                                        </a>
                                    </div>
                                </div>
                                <div className="mt-2">
                                    <input
                                        id="password"
                                        name="password"
                                        type="password"
                                        autoComplete="current-password"
                                        required
                                        className="block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-green-400 sm:text-sm sm:leading-6"
                                    />
                                </div>
                            </div>

                            <div>
                                <button
                                    type="submit"
                                    onClick={handleLogin}
                                    className="flex w-full justify-center rounded-md bg-green-400 px-3 py-1.5 text-sm font-semibold leading-6 text-white shadow-sm hover:bg-green-300 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-green-400"
                                >
                                    Log in
                                </button>
                            </div>
                        </form>

                    </div>
                </div>
            </>
        )

    }
}