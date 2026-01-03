import type { Metadata } from 'next';
import { Inter } from 'next/font/google';
import '@/styles/index.css';

const inter = Inter({ subsets: ['latin'], variable: '--font-inter' });

export const metadata: Metadata = {
  title: 'SchoolConnect Portal',
  description: 'Administration portal for SchoolConnect',
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en" className={inter.variable}>
      <body className="antialiased">{children}</body>
    </html>
  );
}
