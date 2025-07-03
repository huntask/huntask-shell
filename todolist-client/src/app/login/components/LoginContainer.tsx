import React from 'react';
import dynamic from 'next/dynamic';
import { StandOutSectionWithLogo } from '@/components';

export const LoginForm = dynamic(() => import('./LoginForm'), { ssr: true });

export const metadata = {
  title: 'Login',
  description: 'Log into the application',
};

export function LoginContainer() {
  return (
    <StandOutSectionWithLogo className="login">
      <LoginForm />
    </StandOutSectionWithLogo>
  );
}
