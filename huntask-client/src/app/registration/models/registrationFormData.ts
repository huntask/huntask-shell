export type RegistrationFormData = {
  email?: string;
  password?: string;
  confirmPassword?: string;
  firstName?: string;
  lastName?: string;
  avatar?: File | null;
};

export const initialRegistrationFormData: RegistrationFormData = {};
