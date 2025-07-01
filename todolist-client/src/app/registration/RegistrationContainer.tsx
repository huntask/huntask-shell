import React from 'react';
import dynamic from 'next/dynamic';
import { StandOutSectionWithLogo } from '@/components';

const RegistrationForm = dynamic(() => import('./RegistrationForm'), { ssr: true });

export const metadata = {
  title: 'Registration',
  description: 'Register in the application',
};

export function RegistrationContainer() {
  return (
    <StandOutSectionWithLogo className="registration">
      <RegistrationForm />
    </StandOutSectionWithLogo>
  );
}
