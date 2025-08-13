export type RegistrationFormState = {
  success: boolean;
  errors?: string[];
};

export const initialRegistrationFormState: RegistrationFormState = {
  success: false,
};
