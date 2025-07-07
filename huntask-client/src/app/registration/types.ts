export type RegistrationFormState = {
  email?: string;
  password?: string;
  confirmPassword?: string;
  firstName?: string;
  lastName?: string;
  avatar?: string;
  error?: string | null;
};

export const initialRegistrationFormState: RegistrationFormState = {
  email: '',
  password: '',
  confirmPassword: '',
  firstName: '',
  lastName: '',
};
