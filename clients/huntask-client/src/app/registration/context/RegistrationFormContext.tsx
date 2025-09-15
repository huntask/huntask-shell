'use client';
import { createContext, Dispatch, ReactNode, SetStateAction, useState } from 'react';
import { initialRegistrationFormData, RegistrationFormData } from '../models';

type RegistrationFormContextType = {
  formData: RegistrationFormData;
  setFormData: Dispatch<SetStateAction<RegistrationFormData>>;
};

export const RegistrationFormContext = createContext<RegistrationFormContextType>({
  formData: initialRegistrationFormData,
  setFormData: () => {},
});

export function convertToFormData(data: RegistrationFormData): FormData {
  const formData = new FormData();

  Object.keys(data).forEach((prop) => {
    const key = prop as keyof RegistrationFormData;
    formData.append(key, data[key] as string | File);
  });

  return formData;
}

export function RegistrationFormProvider({ children }: { children: ReactNode }) {
  const [formData, setFormData] = useState<RegistrationFormData>(initialRegistrationFormData);
  const initialContextValue = { formData, setFormData };

  return (
    <RegistrationFormContext.Provider value={initialContextValue}>
      {children}
    </RegistrationFormContext.Provider>
  );
}
