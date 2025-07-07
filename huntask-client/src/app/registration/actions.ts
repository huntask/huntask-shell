'use server';

import { RegistrationFormState } from './types';

export async function registrationAction(currentState: RegistrationFormState, formData: FormData) {
  const email = formData.get('email') as string;
  const password = formData.get('password') as string;
  const firstName = formData.get('firstName') as string;
  const lastName = formData.get('lastName') as string;

  if (!email || !password || !firstName || !lastName) {
    return { ...currentState, error: 'Email, password, first name and last name are required.' };
  }

  return {
    ...currentState,
    email,
    password,
    firstName,
    lastName,
    error: null,
  };
}
