'use client';
import React from 'react';
import { RegistrationFormProvider } from './context/RegistrationFormContext';
import RegistrationForm from './RegistrationForm';

export default function RegistrationFormWithContext() {
  return (
    <RegistrationFormProvider>
      <RegistrationForm />
    </RegistrationFormProvider>
  );
}
