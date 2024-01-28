import "./globals.css";
import NavBar from "./components/NavBar";
import * as React from 'react';

type Props = {
  children?: React.ReactNode
};

export default function RootLayout({ children }: Props): React.ReactNode {
  return (
      <main className="px-12">
        <nav className="flex pb-8">
          <NavBar />
        </nav>
        <section className="items-center container mx-auto">{children}</section>
      </main>
  );
}
