'use server';

import { LoginActionState } from './loginActionState';

function isEmailInvalid(email: string): boolean {
  return !email;
}

function isPasswordInvalid(password: string): boolean {
  return !password;
}

export async function loginAction(currentState: LoginActionState, formData: FormData): Promise<LoginActionState> {
  const email = formData.get('email') as string;
  const password = formData.get('password') as string;

  if (isEmailInvalid(email)) {
    return {
      success: false,
      errors: ['Email is not specified.']
    };
  }

  if (isPasswordInvalid(password)) {
    return {
      success: false,
      errors: ['Password is not specified.']
    };
  }

  const loginUrl = `${process.env.IDENTITY_API_HOST}${process.env.IDENTITY_API_VERSION}/identity/login`;

  try {
    const response = await fetch(loginUrl, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ email, password }),
    });

    if (!response.ok) {
      const errorData = await response.json();
      return {
        success: false,
        errors: [errorData.message || 'Login failed.']
      };
    }

    const data = await response.json();
    return { success: true };
  }
  catch (error: any) {
    console.error('Error during login:', error);
    return {
      success: false,
      errors: [error?.message ?? 'Unexpected error']
    };
  }
}
