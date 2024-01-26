import "./globals.css";
import NavBar from "./components/NavBar";
import * as React from 'react';

type Props = {
  children?: React.ReactNode
};

export default function RootLayout({children}: Props): React.ReactNode {
  return (
    <html lang="en">
      <body className="px-4">
        <div className="flex">
          <NavBar />
        </div>
        <main className="md:container md:mx-auto">{children}</main>
      </body>
    </html>
  );
}
