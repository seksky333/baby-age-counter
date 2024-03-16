import { QueryClient, QueryClientProvider, useMutation } from 'react-query';
import { LoginModel, login } from '../network/AuthModule';
import { useState } from 'react';

const queryClient = new QueryClient()

export default function Login() {
    return (
        <QueryClientProvider client={queryClient}>
            <LoginContent />
        </QueryClientProvider>
    )
}

function LoginContent() {
    const {isLoading, isError, isSuccess, mutate} = useMutation({
        mutationFn: (loginModel: LoginModel) => {
          return login(loginModel)
        },
      })
    


    const [loginModel, setLoginModel] = useState<LoginModel>({ email: 'seksky333@gmail.com', password: '60Auburn!' });
    // const { isLoading, isError, data, refetch } = useQuery<IdentityModel>({
    //     queryKey: ['login'],
    //     queryFn: (loginModel: LoginModel) => login(loginModel),
    //     enabled: false
    // });

    // const handleLogin = () => {
    //     refetch();
    // };

    const onLoginModelChanged = (e: React.ChangeEvent<HTMLInputElement>, isEmail: boolean) => {
        const newVal = e.target.value;
        if (isEmail) {
            setLoginModel(
                {
                    email: newVal,
                    password: loginModel.password
                }
            );
        } else {
            setLoginModel(
                {
                    email: loginModel.email,
                    password: newVal
                }
            );
        }
    }

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
                                        value={loginModel.email}
                                        onChange={(e) => onLoginModelChanged(e, true)}
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
                                        value={loginModel.password}
                                        autoComplete="current-password"
                                        required
                                        onChange={(e) => onLoginModelChanged(e, false)}
                                        className="block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-green-400 sm:text-sm sm:leading-6"
                                    />
                                </div>
                            </div>

                            <div>
                                <button
                                    type="submit"
                                    onClick={()=>{
                                        mutate(loginModel)
                                    }}
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

