import "./globals.css";
import NavBar from "./components/NavBar";
import * as React from 'react';

type Props = {
  children?: React.ReactNode
};

export default function RootLayout({ children }: Props): React.ReactNode {
  return (
      <main className="px-4">
        <nav className="flex">
          <NavBar />
        </nav>
        <section className="md:container md:mx-auto">{children}</section>
      </main>
  );
}
