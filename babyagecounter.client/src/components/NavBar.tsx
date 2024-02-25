
export default function NavBar() {
    return (
        <nav className="flex flex-row my-8">
            <section className="basis-2/3">
                <ul className="flex gap-4">
                    <li>
                        <a href="/" className="sm:px-6 px-2 py-1 uppercase font-semibold tracking-wider border-2 text-green-400 hover:underline">
                            Home
                        </a>
                    </li>
                </ul>
            </section>
            <section className="basis-1/3">
                <ul className="flex gap-4 flex-row-reverse">
                    <li>
                        <a href="/Login" className="sm:px-6 px-2 py-1 uppercase font-normal tracking-wider border-2 bg-green-400 text-white hover:underline">
                            Log in
                        </a>
                    </li>
                </ul>
            </section>
        </nav>
    );
}
