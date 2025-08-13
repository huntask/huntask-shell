'use client';

import { convertToFormData } from '../context/RegistrationFormContext';
import { RegistrationFormState, RegistrationFormData } from '../models';

export async function register(currentState: RegistrationFormState, formData: RegistrationFormData) {
  console.info(formData);

  // TODO[validation]: use zod
  if (!formData.email || !formData.password) {
    return { ...currentState, errors: ['Email and password are required.'] };
  }

  const registrationUrl = `${process.env.IDENTITY_API_HOST}${process.env.IDENTITY_API_VERSION}/identity/register`;

  try {
    const response = await fetch(registrationUrl, {
      method: 'POST',
      body: convertToFormData(formData),
    });

    if (!response.ok) {
      const errorData = await response.json();
      return {
        success: false,
        errors: [errorData.message || 'Registration failed.']
      };
    }

    // TODO[registration]: hateoas links will be added to response
    // const data = await response.json();

    return { success: true };
  }
  catch (error: any) {
    console.error('Error during registration:', error);
    return {
      success: false,
      errors: [error?.message ?? 'Unexpected error']
    };
  }
}
