import { createContext, Dispatch, SetStateAction, useState } from 'react';
import { initialRegistrationFormState, RegistrationFormState } from '../types';

type RegistrationFormContextType = {
  formData: RegistrationFormState;
  setFormData: Dispatch<SetStateAction<RegistrationFormState>>;
};

export const RegistrationFormContext = createContext<RegistrationFormContextType>({
  formData: initialRegistrationFormState,
  setFormData: () => {},
});

export function RegistrationFormProvider({ children }: { children: React.ReactNode }) {
  const [formData, setFormData] = useState<RegistrationFormState>(initialRegistrationFormState);
  const initialContextValue = { formData, setFormData };

  return (
    <RegistrationFormContext.Provider value={initialContextValue}>
      {children}
    </RegistrationFormContext.Provider>
  );
}
