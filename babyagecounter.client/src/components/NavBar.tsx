
export default function NavBar() {
    return (
        <nav>
            <ul className="my-8 flex gap-2">
                <li>
                    <a href="/" className="px-6 py-1 uppercase font-semibold tracking-wider border-2 text-green-400 hover:underline">
                        Home
                    </a>
                </li>
                <li>
                    <a href="/" className="px-6 py-1 uppercase font-semibold tracking-wider border-2 text-green-400 hover:underline">
                        Page 2
                    </a>
                </li>
            </ul>
        </nav>
    );
}
